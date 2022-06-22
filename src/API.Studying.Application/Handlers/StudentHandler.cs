using API.Studying.Application.Commands;
using API.Studying.Application.Commands.Student;
using API.Studying.Application.Utils.EmailConfig;
using API.Studying.Application.Utils.Uploads;
using API.Studying.Domain.Entities;
using API.Studying.Domain.Interfaces.Repositories;
using API.Studying.Domain.ValueObjects;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace API.Studying.Application.Handlers
{
    public class StudentHandler : CommandHandler,
                                  IRequestHandler<CreateStudentCommand, ValidationResult>,
                                  IRequestHandler<UpdateStudentCommand, ValidationResult>,
                                  IRequestHandler<UploadImageStudentCommand, ValidationResult>
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly ISendEmailService _sendEmailService;

        public StudentHandler(UserManager<User> userManager,
                              SignInManager<User> signInManager,
                              IUnitOfWork unitOfWork,
                              IConfiguration configuration,
                              ISendEmailService sendEmailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _sendEmailService = sendEmailService;
        }

        private Student GetByCpf(string cpf)
        {
            return _unitOfWork.StudentRepository.GetByCpf(cpf);
        }
        public async Task<ValidationResult> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            bool IsValid = request.IsValid();
            ValidationResult = request.ValidationResult;


            if (IsValid)
            {
                var user = new User() { Email = request.Email, PhoneNumber = request.PhoneNumber, UserName = request.UserName };
                var student = new Student(user, new Name(request.Name, request.LastName), new Cpf(request.Cpf),
                    string.Empty, request.Scholarity, request.Institution, 0, false, false, new List<Document>(), new List<Video>());
                
                user.UpdateParams(student.Id, student);

                
                if (GetByCpf(request.Cpf) != null)
                {
                    ValidationResult.Errors.Add(new ValidationFailure("", "Já existe esse CPF cadastrado!"));
                    return ValidationResult;
                }

                _unitOfWork.StudentRepository.Create(student);
                
                var result = await _userManager.CreateAsync(user, request.Password);

                if (!result.Succeeded)
                {
                    AdicionarErro("Erro ao registrar estudante! ");
                    foreach(var error in result.Errors)
                    {
                        AdicionarErro(error.Description);
                    }
                    return ValidationResult;
                }

                var confirmToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                string emailTemplate = File.ReadAllText(@"Emailstemplates/ConfirmEmail.html");
                emailTemplate = emailTemplate.Replace("{nomeUsuario}", user.UserName);
                emailTemplate = emailTemplate.Replace("{token}", confirmToken);
                emailTemplate = emailTemplate.Replace("{email}", user.Email);
                await _sendEmailService.SendEmailAsync(emailTemplate, "Boas vindas e confirmação de senha!", user.Email);

                _unitOfWork.Commit();

                if (GetByCpf(request.Cpf) == null)
                {
                    AdicionarErro("Erro ao gravar!");
                }

            }

            return ValidationResult;
        }

        public async Task<ValidationResult> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            bool IsValid = request.IsValid();
            ValidationResult = request.ValidationResult;

            if (IsValid)
            {
                var student = _unitOfWork.StudentRepository.GetById(request.Id);

                student.UpdateParams(new Name(request.Name, request.LastName),
                    student.UrlImage, request.Scholarity, request.Institution);

                var user = await _userManager.FindByEmailAsync(request.Email);

                if(user == null)
                {
                    ValidationResult.Errors.Add(new ValidationFailure("", "Usuário inválido!"));
                    return ValidationResult;
                }
                else
                {
                    var resultUserName = await _userManager.SetUserNameAsync(user, request.UserName);
                    if (!resultUserName.Succeeded)
                    {
                        ValidationResult.Errors.Add(new ValidationFailure("", "UserName inválido!"));
                        return ValidationResult;
                    }

                    var resultPhone = await _userManager.SetPhoneNumberAsync(user, request.PhoneNumber);
                    if (!resultPhone.Succeeded)
                    {
                        ValidationResult.Errors.Add(new ValidationFailure("", "UserName inválido!"));
                        return ValidationResult;
                    }
                }

                _unitOfWork.StudentRepository.Update(student);

                if (!_unitOfWork.Commit())
                    AdicionarErro("Erro ao gravar!");
            }

            return ValidationResult;
        }

        public async Task<ValidationResult> Handle(UploadImageStudentCommand request, CancellationToken cancellationToken)
        {
            var imageUrl = string.Empty;
            ValidationResult = new ValidationResult();

            try
            {
                var student = _unitOfWork.StudentRepository.GetById(request.StudentId);

                imageUrl = await UploadImageService.UploadBase64ImageAsync(request.ImageStringBase64, _configuration["AzureBlob:Connection"], _configuration["AzureBlob:ImageContainer"]);

                student.UpdatePhoto(imageUrl);

                _unitOfWork.StudentRepository.Update(student);

                if (!_unitOfWork.Commit())
                    AdicionarErro("Erro ao gravar!");       
            }
            catch(Exception ex)
            {
                AdicionarErro("Imagem inválida!" + ex.Message);
            }

            return ValidationResult;
        }
    }
}
