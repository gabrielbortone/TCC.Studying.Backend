using FluentValidation;
using System;
using System.Text.Json.Serialization;

namespace API.Studying.Application.Commands.Suport
{
    public class CreateSuportRequestCommand : CommandBase
    {
        public Guid? StudentId { get; set; }
        public string Email { get; set; }
        public int TypeOfSuport { get; set; }
        public string Text { get; set; }
        
        [JsonConstructor]
        public CreateSuportRequestCommand(Guid? studentId, string email, int typeOfSuport, string text)
        {
            StudentId = studentId;
            Email = email;
            TypeOfSuport = typeOfSuport;
            Text = text;
        }

        public override bool IsValid()
        {
            ValidationResult = new CreateSuportRequestCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }

    }

    public class CreateSuportRequestCommandValidation : AbstractValidator<CreateSuportRequestCommand>
    {
        public CreateSuportRequestCommandValidation()
        {
            RuleFor(c => c.TypeOfSuport)
                .ExclusiveBetween(0, 6)
                .WithMessage("O suporte só pode ser do tipo 0 a 6");

            RuleFor(c=> c.Email)
                .EmailAddress()
                .WithMessage("O email precisa ser válido");

            RuleFor(a => a.Text)
                .NotEqual(string.Empty).NotNull()
                .WithMessage("O texto é obrigatório!");
        }
    }
}
