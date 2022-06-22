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
                .WithMessage("O nome � obrigat�rio");
                        
            RuleFor(c => c.LastName)
                .MaximumLength(50)
                .WithMessage("O sobrenome precisa ter menos de 50 caracteres");
            
            RuleFor(c => c.LastName)
                .NotEqual(string.Empty).NotNull()
                .WithMessage("O sobrenome precisa ser v�lido");

            RuleFor(c => c.Email)
                .MaximumLength(50)
                .WithMessage("Email precisa ter menos de 50 caracteres");

            RuleFor(c => c.Email)
                .EmailAddress()
                .WithMessage("Email precisa ser v�lido");

            RuleFor(c => c.Email)
                .NotEqual(string.Empty).NotNull()
                .WithMessage("Email � necess�rio");

            RuleFor(c => c.UserName)
                .MaximumLength(30)
                .WithMessage("UserName pode conter no m�ximo 30 caracteres");
            
            RuleFor(c => c.UserName)
                .NotEqual(string.Empty).NotNull()
                .WithMessage("Username � obrigat�rio");

            RuleFor(c => c.PhoneNumber)
                .MaximumLength(12)
                .WithMessage("O telefone precisa ter menos de 12 n�meros");
            
            RuleFor(c => c.PhoneNumber)
                .NotEqual(string.Empty).NotNull()
                .WithMessage("O telefone � obrigat�rio");
            
            RuleFor(c => c.Scholarity)
                .MaximumLength(45)
                .WithMessage("A escolaridade precisa ter menos de 45 caracteres");
            
            RuleFor(c => c.Scholarity)
                .NotEqual(string.Empty).NotNull()
                .WithMessage("A escolaridade � obrigat�ria");
            
            RuleFor(c => c.Institution)
                .MaximumLength(50)
                .WithMessage("A Institui��o precisa ter no m�ximo 50 caracteres");
            
            RuleFor(c => c.Institution)
                .NotEqual(string.Empty).NotNull()
                .WithMessage("A Institui��o � obrigat�ria");
        }
    }   
}