using System;

namespace API.Studying.Domain.Entities.PreferencesUser
{
    public class StudentDocument : Entity
    {
        public Guid StudentId { get; set; }
        public Guid DocumentId { get; set; }
        public StudentDocument(){}
        public StudentDocument(Guid studentId, Guid documentId)
        {
            StudentId = studentId;
            DocumentId = documentId;
        }
    }
}
