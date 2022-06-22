using FluentValidation;
using Newtonsoft.Json;
using System;

namespace API.Studying.Application.Commands
{
    public class UpdateStudentCommand : CommandBase
    {
        public Guid Id { get; set; }
        public string Name {get; private set;}
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string UserName { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Scholarity { get; private set; }
        public string Institution { get; private set; }

        [JsonConstructor]
        public UpdateStudentCommand(Guid id, string name, string lastName, string email, string userName, string phoneNumber, string scholarity, string institution)
        {
            Id = id;
            Name = name;
            LastName = lastName;
            Email = email;
            UserName = userName;
            PhoneNumber = phoneNumber;
            Scholarity = scholarity;
            Institution = institution;
        }
        public override bool IsValid()
        {
            ValidationResult = new UpdateStudentCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class UpdateStudentCommandValidator : AbstractValidator<UpdateStudentCommand>
    {
        public UpdateStudentCommandValidator()
        { 
            RuleFor(c => c.Name)
                .MaximumLength(50)
                .WithMessage("O nome precisa ter menos de 50 caracteres");
            
            RuleFor(c => c.Name)
                .NotEqual(string.Empty).NotNull()
                .WithMessage("O nome é obrigatório");
                        
            RuleFor(c => c.LastName)
                .MaximumLength(50)
                .WithMessage("O sobrenome precisa ter menos de 50 caracteres");
            
            RuleFor(c => c.LastName)
                .NotEqual(string.Empty).NotNull()
                .WithMessage("O sobrenome precisa ser válido");

            RuleFor(c => c.Email)
                .MaximumLength(50)
                .WithMessage("Email precisa ter menos de 50 caracteres");

            RuleFor(c => c.Email)
                .EmailAddress()
                .WithMessage("Email precisa ser válido");

            RuleFor(c => c.Email)
                .NotEqual(string.Empty).NotNull()
                .WithMessage("Email é necessário");

            RuleFor(c => c.UserName)
                .MaximumLength(30)
                .WithMessage("UserName pode conter no máximo 30 caracteres");
            
            RuleFor(c => c.UserName)
                .NotEqual(string.Empty).NotNull()
                .WithMessage("Username é obrigatório");

            RuleFor(c => c.PhoneNumber)
                .MaximumLength(12)
                .WithMessage("O telefone precisa ter menos de 12 números");
            
            RuleFor(c => c.PhoneNumber)
                .NotEqual(string.Empty).NotNull()
                .WithMessage("O telefone é obrigatório");
            
            RuleFor(c => c.Scholarity)
                .MaximumLength(45)
                .WithMessage("A escolaridade precisa ter menos de 45 caracteres");
            
            RuleFor(c => c.Scholarity)
                .NotEqual(string.Empty).NotNull()
                .WithMessage("A escolaridade é obrigatória");
            
            RuleFor(c => c.Institution)
                .MaximumLength(50)
                .WithMessage("A Instituição precisa ter no máximo 50 caracteres");
            
            RuleFor(c => c.Institution)
                .NotEqual(string.Empty).NotNull()
                .WithMessage("A Instituição é obrigatória");
        }
    }   
}