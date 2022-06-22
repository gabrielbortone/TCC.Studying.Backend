using System;
using FluentValidation;
using Newtonsoft.Json;

namespace API.Studying.Application.Commands
{
    public class FavoriteTopicCommand : CommandBase
    {
        public Guid StudentId { get; set; }
        public Guid TopicId { get; private set; }

        [JsonConstructor]
        public FavoriteTopicCommand(Guid studentId, Guid topicId)
        {
            StudentId = studentId;
            TopicId = topicId;
        }
        public override bool IsValid()
        {
            ValidationResult = new FavoriteTopicCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class FavoriteTopicCommandValidator : AbstractValidator<FavoriteTopicCommand>
    {
        public FavoriteTopicCommandValidator()
        { 
            RuleFor(c => c.StudentId)
                .NotEqual(Guid.Empty)
                .WithMessage("Estudante precisa ser válido");

            RuleFor(c => c.TopicId)
                .NotEqual(Guid.Empty)
                .WithMessage("O tópico precisa ser válido");
        }
    }
}