using FluentValidation;
using Newtonsoft.Json;
using System;

namespace API.Studying.Application.Commands
{
    public class CreateDocumentCommand : CommandBase
    {
        public Guid TopicId { get; private set; }
        public Guid StudentId { get; set; }
        public string Title { get; private set; }
        public string DocumentBase64 { get; private set; }
        public string Keys { get; private set; }

        [JsonConstructor]
        public CreateDocumentCommand(Guid topicId, Guid studentId, string title, string documentBase64, string keys)
        {
            TopicId = topicId;
            StudentId = studentId;
            Title = title;
            DocumentBase64 = documentBase64;
            Keys = keys;
        }
        public override bool IsValid()
        {
            ValidationResult = new CreateDocumentCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class CreateDocumentCommandValidator : AbstractValidator<CreateDocumentCommand>
    {
        public CreateDocumentCommandValidator()
        {
            RuleFor(c => c.StudentId)
                .NotEqual(Guid.Empty)
                .WithMessage("Estudante inválido");
            
            RuleFor(c => c.TopicId)
                .NotEqual(Guid.Empty)
                .WithMessage("Tópico inválido");
                
            RuleFor(c => c.Title)
                .MaximumLength(125)
                .WithMessage("O título deve conter menos de 125 caracteres");
            
            RuleFor(c => c.Title)
                .NotEqual(string.Empty).NotNull()
                .WithMessage("O título é obrigatório");

            RuleFor(c => c.Keys)
                .MaximumLength(128)
                .WithMessage("As palavras-chaves devem ter menos de 128 caracteres");
        }
    }
}