using FluentValidation.Results;
using System;
using System.Text.Json.Serialization;

namespace API.Studying.Application.Commands.Admin
{
    public class AdminRemoveDocumentCommand : CommandBase
    {
        public Guid DocumentId { get; set; }

        [JsonConstructor]
        public AdminRemoveDocumentCommand(Guid documentId)
        {
            DocumentId = documentId;
            if (DocumentId == null || DocumentId == Guid.NewGuid())
            {
                ValidationResult.Errors.Add(new ValidationFailure("", "Documento inválido!"));
            }
        }

        public override bool IsValid()
        {
            return ValidationResult.IsValid;
        }
    }
}
