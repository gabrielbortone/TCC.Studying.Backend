using System;
using FluentValidation;
using Newtonsoft.Json;

namespace API.Studying.Application.Commands
{
    public class FavoriteDocumentCommand : CommandBase
    {
        public Guid DocumentId {get; set;}
        public Guid StudentId { get; set; }
        
        [JsonConstructor]
        public FavoriteDocumentCommand(Guid documentId, Guid studentId)
        {
            DocumentId = documentId;
            StudentId = studentId;
        }
        public override bool IsValid()
        {
            ValidationResult = new FavoriteDocumentCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class FavoriteDocumentCommandValidator : AbstractValidator<FavoriteDocumentCommand>
    {
        public FavoriteDocumentCommandValidator()
        {
            RuleFor(c => c.StudentId)
                .NotEqual(Guid.Empty)
                .WithMessage("StudentId is required");

            RuleFor(c => c.DocumentId)
                .NotEqual(Guid.Empty)
                .WithMessage("TopicId is required");
        }
    }
}