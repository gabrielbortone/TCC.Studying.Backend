using API.Studying.Application.Commands;
using API.Studying.Application.Commands.Account;
using API.Studying.Application.DTOs;
using API.Studying.Application.Utils.EmailConfig;
using API.Studying.Application.Utils.Jwt;
using API.Studying.Domain.Entities;
using API.Studying.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace API.Studying.Application.Handlers
{
    public class AccountHandler : CommandHandler,
                                  IRequestHandler<AccountLoginCommand, TokenDto>,
                                  IRequestHandler<ConfirmEmailCommand, ValidationResult>,
                                  IRequestHandler<ChangePasswordCommand, ValidationResult>,
                                  IRequestHandler<RecoverPasswordCommand, ValidationResult>,
                                  IRequestHandler<RecoverPasswordConfirmedCommand, ValidationResult>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IRecoverPasswordRepository _recoverPasswordRepository;
        private readonly ISendEmailService _sendEmailService;
        private readonly IUnitOfWork _unitOfWork;

        public AccountHandler(IHttpContextAccessor httpContextAccessor,
                              UserManager<User> userManager, 
                              SignInManager<User> signInManager,
                              IRecoverPasswordRepository recoverPasswordRepository,
                              ISendEmailService sendEmailService,
                              IUnitOfWork unitOfWork)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _signInManager = signInManager;
            _recoverPasswordRepository = recoverPasswordRepository;
            _sendEmailService = sendEmailService;
            _unitOfWork = unitOfWork;
        }
        
        private async Task<TokenDto> GetTokenDtoAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (!user.EmailConfirmed)
            {
                throw new ArgumentException("O e-mail precisa ser confirmado antes!");
            }
            var student = _unitOfWork.StudentRepository.GetById(user.StudentId);
            if (student == null)
            {
                throw new ArgumentException("Não foi possível encontrar estudante!");
            }
            if (student.IsDeleted || student.IsBlocked)
            {
                throw new ArgumentException("Não foi possível logar com esse estudante!");
            }
            var tokenDto = new TokenDto(TokenService.GenerateToken(user), new StudentDto(student.Id, student.Name.FirstName, student.Name.LastName, student.UrlImage, student.IsAdmin, student.Point, student.IdentityUser.Email, student.IdentityUser.UserName, student.IsDeleted, student.IsBlocked), DateTime.Now.AddHours(4));
            return tokenDto;
        }

        public async Task<TokenDto> Handle(AccountLoginCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                throw new ArgumentException("Usuário/Senha Inválido(s)");
            }
            else
            {
                var result = await _signInManager.PasswordSignInAsync(request.UserName,
                   request.Password, true, lockoutOnFailure: true);

                if (result.Succeeded)
                {
                    return await GetTokenDtoAsync(request.UserName);
                }
                else
                {
                    throw new ArgumentException("Usuário/Senha Inválido(s)");
                }
            }
        }

        public async Task<ValidationResult> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var result = await _signInManager.PasswordSignInAsync(request.UserName, request.OldPassword, false, true);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(request.UserName);
                if (request.NewPassword == request.ConfirmNewPassword)
                {
                    var resultChangePassword = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);
                    if (!resultChangePassword.Succeeded)
                    {
                        foreach (var error in resultChangePassword.Errors)
                        {
                            AdicionarErro(error.Description);
                        } 
                    }
                }
                else
                {
                    AdicionarErro("As senhas não são iguais!");
                }
            }
            else
            {
                AdicionarErro("Erro a senha antiga ou userName é/são inválido(s)!");
            }

            return ValidationResult;
        }

        private async Task RecoverPasswordPhase1(User user, string userName)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            RecoverPassword recoverPassword = new RecoverPassword(token, user.Email, userName, DateTime.Now);
            _recoverPasswordRepository.Create(recoverPassword);
            string emailTemplate = File.ReadAllText(@"EmailsTemplates/RecoverPasswordPhase1.html");
            emailTemplate = emailTemplate.Replace("{nomeUsuario}", user.UserName);
            emailTemplate = emailTemplate.Replace("{token}", token);
            await _sendEmailService.SendEmailAsync(emailTemplate, "Recuperação de senha", user.Email);
        }

        public async Task<ValidationResult> Handle(RecoverPasswordCommand request, CancellationToken cancellationToken)
        {
            ValidationResult = new ValidationResult();
            var user = await _userManager.FindByNameAsync(request.UserName);
            
            if(user == null)
            {
                AdicionarErro("Usuário inválido!");
            }
            else
            {
                await RecoverPasswordPhase1(user, request.UserName);
            }
            
            return ValidationResult;
        }

        public async Task<ValidationResult> Handle(RecoverPasswordConfirmedCommand request, CancellationToken cancellationToken)
        {
            ValidationResult = new ValidationResult();
            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null)
            {
                AdicionarErro("Usuário inválido!");
            }
            else
            {
                var recoverPassword = _recoverPasswordRepository.GetByEmail(user.Email);
                if(recoverPassword.Code != request.Number)
                {
                    AdicionarErro("Os tokens são incompatíveis!");
                    return ValidationResult;
                }
                else
                {
                    var result = await _userManager.ResetPasswordAsync(user, request.Number, request.NewPassword);
                    if (!result.Succeeded)
                    {
                        AdicionarErro("Não foi possível resetar a senha!");
                        foreach(var error in result.Errors)
                        {
                            AdicionarErro(error.Description);
                        }
                    }
                }
            }

            return ValidationResult;
        }

        public async Task<ValidationResult> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            var result = await _userManager.ConfirmEmailAsync(user, request.Code);
            if (!result.Succeeded)
            {
                foreach (var message in result.Errors)
                {
                    AdicionarErro(message.Description);
                }
            }

            return ValidationResult;
        }
    }
}
