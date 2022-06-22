using System;
using FluentValidation;
using Newtonsoft.Json;

namespace API.Studying.Application.Commands
{
    public class UpdateDocumentCommand : CommandBase
    {
        public Guid Id { get; set; }
        public Guid TopicId { get; set; }
        public Guid StudentId { get; set; }
        public string Title { get; set; }
        public string UrlDocument { get; set; }
        public string Keys { get; set; }

        [JsonConstructor]
        public UpdateDocumentCommand(Guid id, Guid topicId, Guid studentId, string title, string urlDocument, string keys)
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
            ValidationResult = new UpdateDocumentCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
    public class UpdateDocumentCommandValidator : AbstractValidator<UpdateDocumentCommand>
    {
        public UpdateDocumentCommandValidator()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("O id � extremamente necess�rio");

            RuleFor(c => c.TopicId)
                .NotEqual(Guid.Empty)
                .WithMessage("O t�pico � inv�lido");

            RuleFor(c => c.StudentId)
                .NotEqual(Guid.Empty)
                .WithMessage("O estudante � inv�lido");

            RuleFor(c => c.Title)
                .MaximumLength(125)
                .WithMessage("O t�tulo deve ter menos de 125 caracteres");
            
            RuleFor(c => c.Title)
                .NotEqual(string.Empty).NotNull()
                .WithMessage("O t�tulo � obrigat�rio");

            RuleFor(c => c.Keys)
                .MaximumLength(128)
                .WithMessage("As palavras-chaves devem ter menos de 128 caracteres");
        }
    }
}