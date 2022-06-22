using FluentValidation;
using System;
using System.Text.Json.Serialization;

namespace API.Studying.Application.Commands.Admin
{
    public class AdminUpdateTopicCommand : CommandBase
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public Guid? FatherTopicId { get; private set; }

        [JsonConstructor]
        public AdminUpdateTopicCommand(Guid id, string title, string description, Guid? fatherTopicId)
        {
            Id = id;
            Title = title;
            Description = description;
            FatherTopicId = fatherTopicId;
        }
        public override bool IsValid()
        {
            ValidationResult = new AdminUpdateTopicCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class AdminUpdateTopicCommandValidator : AbstractValidator<AdminUpdateTopicCommand>
    {
        public AdminUpdateTopicCommandValidator()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("O id precisa ser obrigatório");

            RuleFor(c => c.Title)
                .MaximumLength(125)
                .WithMessage("O título precisa ter menos de 125 caracteres");

            RuleFor(c => c.Title)
                .NotEqual(string.Empty).NotNull()
                .WithMessage("O título é obrigatório");

            RuleFor(c => c.Description)
                .MaximumLength(500)
                .WithMessage("A descrição precisa ter menos de 500 caracteres");

            RuleFor(c => c.Description)
                .NotEqual(string.Empty).NotNull()
                .WithMessage("A descrição precisa ser preenchida");
        }
    }
}
