using System;
using System.Text.Json.Serialization;

namespace API.Studying.Application.Commands.Document
{
    public class CancelStarDocumentCommand : CommandBase
    {
        public Guid StudentId { get; set; }
        public Guid DocumentId { get; set; }
        [JsonConstructor]
        public CancelStarDocumentCommand(Guid studentId, Guid documentId)
        {
            StudentId = studentId;
            DocumentId = documentId;
        }
    }
}
