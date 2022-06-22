using System;

namespace API.Studying.Application.Commands.Document
{
    public class StarDocumentCommand : CommandBase
    {
        public Guid StudentId { get; set; }
        public Guid DocumentId { get; set; }
        public StarDocumentCommand(Guid studentId, Guid documentId)
        {
            StudentId = studentId;
            DocumentId = documentId;
        }
    }
}
