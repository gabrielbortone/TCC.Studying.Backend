using System;

namespace API.Studying.Domain.Entities.PreferencesUser.Star
{
    public class StarDocument : Entity
    {
        public Guid StudentId { get; set; }
        public Guid DocumentId { get; set; }

        public StarDocument(){}
        public StarDocument(Guid studentId, Guid documentId)
        {
            StudentId = studentId;
            DocumentId = documentId;
        }
    }
}
