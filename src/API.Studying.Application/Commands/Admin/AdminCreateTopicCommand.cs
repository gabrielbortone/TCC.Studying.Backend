using FluentValidation;
using System;
using System.Text.Json.Serialization;

namespace API.Studying.Application.Commands.Admin
{
    public class AdminCreateTopicCommand : CommandBase
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public Guid? FatherTopicId { get; private set; }

        [JsonConstructor]
        public AdminCreateTopicCommand(string title, string description, Guid? fatherTopicId)
        {
            Title = title;
            Description = description;
            FatherTopicId = fatherTopicId;
        }
        public override bool IsValid()
        {
            ValidationResult = new AdminCreateTopicCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }

    }

    public class AdminCreateTopicCommandValidator : AbstractValidator<AdminCreateTopicCommand>
    {
        public AdminCreateTopicCommandValidator()
        {

            RuleFor(c => c.Title)
                .MaximumLength(125)
                .WithMessage("O título deve conter menos de 125 caracteres");

            RuleFor(c => c.Title)
                .NotEqual(string.Empty).NotNull()
                .WithMessage("O título é obrigatório");

            RuleFor(c => c.Description)
                .MaximumLength(500)
                .WithMessage("A descrição precisa ter menos de 500 caracteres");

            RuleFor(c => c.Description)
                .NotEqual(string.Empty).NotNull()
                .WithMessage("A descrição é necessária");
        }
    }
}
