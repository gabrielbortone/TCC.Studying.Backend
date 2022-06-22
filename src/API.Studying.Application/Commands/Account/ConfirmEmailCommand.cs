using FluentValidation;

namespace API.Studying.Application.Commands.Account
{
    public class ConfirmEmailCommand : CommandBase
    {
        public string Email { get; set; }
        public string Code { get; set; }

        public ConfirmEmailCommand(string email, string code)
        {
            Email = email;
            Code = code;
        }
    }

    public class ConfirmEmailCommandValidator : AbstractValidator<ConfirmEmailCommand>
    {
        public ConfirmEmailCommandValidator()
        {
            RuleFor(c => c.Email)
                .EmailAddress()
                .WithMessage("Email inválido!");

            RuleFor(c => c.Code)
                .NotEmpty()
                .WithMessage("O ´código precisa estar preenchido!");
        }
    }
}
