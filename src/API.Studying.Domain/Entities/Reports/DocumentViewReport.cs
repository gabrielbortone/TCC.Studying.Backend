using System;

namespace API.Studying.Domain.Entities.Reports
{
    public class DocumentViewReport : Entity
    {
        public Student Student { get; set; }
        public Document Document { get; set; }
        public DateTime Date { get; set; }
        public DocumentViewReport() { }
        public DocumentViewReport(Student student, Document document, DateTime date)
        {
            Student = student;
            Document = document;
            Date = date;
        }
    }
}
