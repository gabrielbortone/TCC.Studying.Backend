using FluentValidation;
using System;
using System.Text.Json.Serialization;

namespace API.Studying.Application.Commands.Admin
{
    public class AdminUpdateDocumentCommand : CommandBase
    {
        public Guid Id { get; set; }
        public Guid TopicId { get; set; }
        public Guid StudentId { get; set; }
        public string Title { get; set; }
        public string UrlDocument { get; set; }
        public string Keys { get; set; }

        [JsonConstructor]
        public AdminUpdateDocumentCommand(Guid id, Guid topicId, Guid studentId, string title, string urlDocument, string keys)
        {
            Id = id;
            TopicId = topicId;
            StudentId = studentId;
            Title = title;
            UrlDocument = urlDocument;
            Keys = keys;
        }
        public override bool IsValid()
        {
            ValidationResult = new AdminUpdateDocumentCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class AdminUpdateDocumentCommandValidator : AbstractValidator<AdminUpdateDocumentCommand>
    {
        public AdminUpdateDocumentCommandValidator()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("O id é extremamente necessário");

            RuleFor(c => c.TopicId)
                .NotEqual(Guid.Empty)
                .WithMessage("O tópico é inválido");

            RuleFor(c => c.StudentId)
                .NotEqual(Guid.Empty)
                .WithMessage("O tópico é inválido");

            RuleFor(c => c.Title)
                .MaximumLength(125)
                .WithMessage("O título deve ter menos de 125 caracteres");

            RuleFor(c => c.Title)
                .NotEqual(string.Empty).NotNull()
                .WithMessage("O título é obrigatório");

            RuleFor(c => c.Keys)
                .MaximumLength(128)
                .WithMessage("As palavras-chaves devem ter menos de 128 caracteres");
        }
    }
}
