using FluentValidation;
using System;
using System.Text.Json.Serialization;

namespace API.Studying.Application.Commands.Document
{
    public class UnfavoriteDocumentCommand : CommandBase
    {
        public Guid StudentId { get; set; }
        public Guid DocumentId { get; set; }
        [JsonConstructor]
        public UnfavoriteDocumentCommand(Guid studentId, Guid documentId)
        {
            StudentId = studentId;
            DocumentId = documentId;
        }

        public override bool IsValid()
        {
            ValidationResult = new UnfavoriteDocumentCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }

    }

    public class UnfavoriteDocumentCommandValidator : AbstractValidator<UnfavoriteDocumentCommand>
    {
        public UnfavoriteDocumentCommandValidator()
        {
            RuleFor(c => c.StudentId)
                .NotEqual(Guid.Empty)
                .WithMessage("Estudante Inválido");

            RuleFor(c => c.DocumentId)
                .NotEqual(Guid.Empty)
                .WithMessage("Documento inválido");
        }
    }
}
