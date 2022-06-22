using API.Studying.Domain.CustomExceptions;
using API.Studying.Domain.Entities.Reports;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Studying.Domain.Entities
{
    public class Topic : Entity
    {
        public string Title { get; private set; }
        public string Description { get; private set; }

        [ForeignKey("FatherTopic")]
        public Guid? FatherTopicId { get; private set; }
        public virtual Topic FatherTopic { get; private set; }
        public bool IsDeleted { get; private set; } = false;
        public ICollection<Document> Documents { get; private set; }
        public ICollection<Video> Videos { get; private set; }

        [NotMapped]
        public List<Topic> Sons { get; set; }

        [NotMapped]
        public bool IsFavorite { get; set; }

        public Topic(){}
        public Topic(string title, string description, Guid? fatherTopicId, Topic fatherTopic, 
            ICollection<Document> documents, 
            ICollection<Video> videos)
        {
            Title = title;
            Description = description;
            FatherTopicId = fatherTopicId;
            FatherTopic = fatherTopic;
            Documents = documents;
            Videos = videos;

            if (!IsValid())
                throw new DomainException(nameof(Topic), nameof(this.Id), "Um ou mais erros encontrados na entidade!", nameof(this.IsValid));
        }

        public void DeleteTopic()
        {
            if (IsDeleted)
            {
                IsDeleted = false;
            }
            else
            {
                IsDeleted = true;
            }
        }

        public void UpdateParams(Guid? fatherTopicId, Topic fatherTopic, string title, string description)
        {
            FatherTopicId = fatherTopicId;
            FatherTopic = fatherTopic;
            Title = title;
            Description = description;
        }

        public override bool IsValid()
        {
            var validator = new TopicValidator().Validate(this);
            return validator.IsValid;
        }
    }

    public class TopicValidator : AbstractValidator<Topic>
    {
        public TopicValidator()
        {
            RuleFor(c => c.Title)
               .MaximumLength(125)
               .WithMessage("Title should be less than 125");

            RuleFor(c => c.Title)
                .NotEqual(string.Empty).NotNull()
                .WithMessage("Title is required");

            RuleFor(c => c.Description)
                .MaximumLength(500)
                .WithMessage("Description should be less than 500");

            RuleFor(c => c.Description)
                .NotEqual(string.Empty).NotNull()
                .WithMessage("Description is required");
        }
    }
}
