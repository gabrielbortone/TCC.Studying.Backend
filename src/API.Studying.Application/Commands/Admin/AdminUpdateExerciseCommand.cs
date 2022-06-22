using FluentValidation;
using FluentValidation.Results;
using System;
using System.Text.Json.Serialization;

namespace API.Studying.Application.Commands.Admin
{
    public class AdminUpdateExerciseCommand : CommandBase
    {
        public Guid Id { get; private set; }
        public Guid TopicId { get; private set; }
        public Guid StudentId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public int Type { get; private set; }
        public string Question { get; private set; }
        public string Keys { get; private set; }

        [JsonConstructor]
        public AdminUpdateExerciseCommand(Guid id, Guid topicId, Guid studentId, string title, string description, int type, string question, string keys)
        {
            Id = id;
            TopicId = topicId;
            StudentId = studentId;
            Title = title;
            Description = description;
            Type = type;
            Question = question;
            Keys = keys;
        }
        public override bool IsValid()
        {
            ValidationResult = new AdminUpdateExerciseCommandValidator().Validate(this);
            if (!this.Type.Equals(1) && !this.Type.Equals(2))
            {
                ValidationResult.Errors.Add(new ValidationFailure("Type", "Tipo de exercício precisa ser 1 ou 2"));
            }
            return ValidationResult.IsValid;
        }
    }

    public class AdminUpdateExerciseCommandValidator : AbstractValidator<AdminUpdateExerciseCommand>
    {
        public AdminUpdateExerciseCommandValidator()
        {
            RuleFor(c => c.Id)
               .NotEqual(Guid.Empty)
               .WithMessage("O id é obrigatório");

            RuleFor(c => c.TopicId)
                .NotEqual(Guid.Empty)
                .WithMessage("Tópico inválido");

            RuleFor(c => c.StudentId)
                .NotEqual(Guid.Empty)
                .WithMessage("Estudante inválido");

            RuleFor(c => c.Title)
                .MaximumLength(125)
                .WithMessage("Título deve ser menor que 125 caracteres");

            RuleFor(c => c.Title)
                .NotEqual(string.Empty).NotNull()
                .WithMessage("O título é obrigatório");

            RuleFor(c => c.Description)
                .MaximumLength(500)
                .WithMessage("A descrição deve ser menor que 500 caracteres");

            RuleFor(c => c.Description)
                .NotEqual(string.Empty).NotNull()
                .WithMessage("A descrição é obrigatória");

            RuleFor(c => c.Question)
                .NotEqual(string.Empty).NotNull()
                .WithMessage("A questão é obrigatória");

            RuleFor(c => c.Question)
                 .MaximumLength(1024)
                 .WithMessage("A questão deve conter menos de 1024 caracteres");

            RuleFor(c => c.Keys)
                .MaximumLength(128)
                .WithMessage("As palavras-chaves devem ter menos de 128 caracteres");
        }
    }
}
