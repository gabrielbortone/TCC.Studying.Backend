using API.Studying.Application.DTOs;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Newtonsoft.Json;

namespace API.Studying.Application.Commands
{
    public class AccountLoginCommand : MessageBase, IRequest<TokenDto>
    {
        public string UserName { get; private set; }
        public string Password { get; private set; }
        public ValidationResult ValidationResult { get; set; }

        [JsonConstructor]
        public AccountLoginCommand(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }

        public bool IsValid()
        {
            ValidationResult = new AccountLoginCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class AccountLoginCommandValidation : AbstractValidator<AccountLoginCommand>
    {
        public AccountLoginCommandValidation()
        {
            RuleFor(a=> a.UserName)
                .NotEqual(string.Empty).NotNull()
                .WithMessage("UserName é obrigatório!");
            
            RuleFor(a=> a.Password)
                .NotEqual(string.Empty).NotNull()
                .WithMessage("Senha é obrigatória!");
        }
    }
}