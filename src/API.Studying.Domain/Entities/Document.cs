using API.Studying.Domain.Entities.Reports;
using FluentValidation;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Studying.Domain.Entities
{
    public class Document : Entity
    {
        public Topic Topic { get; private set; }
        public Student Student { get; private set; }
        public string Title { get; private set; }
        public string UrlDocument { get; private set; }
        public int Stars { get; private set; }
        public string Keys { get; private set; }
        public int Views { get; private set; } = 0;
        public ICollection<DocumentViewReport> DocumentViewReports { get; set; }
        
        [NotMapped]
        public bool IsFavorite { get; set; }
        
        public Document() { }
        public Document(Topic topic, Student student, string title, 
            string urlDocument, int stars, string keys, ICollection<DocumentViewReport> documentViewReports)
        {
            Topic = topic;
            Student = student;
            Title = title;
            UrlDocument = urlDocument;
            Stars = stars;
            Keys = keys;
            DocumentViewReports = documentViewReports;
        }

        public void UpdateParams(Topic topic, string title, string urlDocument, string keys)
        {
            Topic = topic;
            Title = title;
            UrlDocument = urlDocument;
            Keys = keys;
        }
        public void UpdateStars(int stars)
        {
            Stars += stars;
            if (Stars <= 0)
                Stars = 0;
        }

        public void IncreaseView()
        {
            Views++;
        }

        public override bool IsValid()
        {
            var validator = new DocumentValidator().Validate(this);
            return validator.IsValid;
        }
    }

    public class DocumentValidator : AbstractValidator<Document>
    {
        public DocumentValidator()
        {
            RuleFor(c => c.Title)
                .MaximumLength(125)
                .WithMessage("Title should be less than 125");

            RuleFor(c => c.Title)
                .NotEqual(string.Empty).NotNull()
                .WithMessage("Title is required");
        }
    }


}
