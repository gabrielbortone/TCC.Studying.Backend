using FluentValidation;
using System;
using System.Text.Json.Serialization;

namespace API.Studying.Application.Commands.Document
{
    public class RemoveDocumentCommand : CommandBase
    {
        public Guid DocumentId { get; set; }
        public Guid StudentId { get; set; }

        public RemoveDocumentCommand(){}

        [JsonConstructor]
        public RemoveDocumentCommand(Guid documentId, Guid studentId)
        {
            DocumentId = documentId;
            StudentId = studentId;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveDocumentCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }

    }

    public class RemoveDocumentCommandValidator : AbstractValidator<RemoveDocumentCommand>
    {
        public RemoveDocumentCommandValidator()
        {
            RuleFor(c => c.DocumentId)
                .NotEqual(Guid.Empty)
                .WithMessage("Documento inválido!");

            RuleFor(c => c.StudentId)
                .NotEqual(Guid.Empty)
                .WithMessage("Estudante inválido!");
        }
    }
}
