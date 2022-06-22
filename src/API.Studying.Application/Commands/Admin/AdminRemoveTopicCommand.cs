using FluentValidation.Results;
using System;
using System.Text.Json.Serialization;

namespace API.Studying.Application.Commands.Admin
{
    public class AdminRemoveTopicCommand : CommandBase
    {
        public Guid TopicId { get; set; }

        [JsonConstructor]
        public AdminRemoveTopicCommand(Guid topicId)
        {
            TopicId = topicId;
            if (TopicId == null || TopicId == Guid.NewGuid())
            {
                ValidationResult.Errors.Add(new ValidationFailure("", "Tópico inválido!"));
            }
        }

        public override bool IsValid()
        {
            return ValidationResult.IsValid;
        }
    }
}
