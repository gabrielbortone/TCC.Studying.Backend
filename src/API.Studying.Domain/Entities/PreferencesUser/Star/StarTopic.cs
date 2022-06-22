using System;

namespace API.Studying.Domain.Entities.PreferencesUser.Star
{
    public class StarTopic : Entity
    {
        public Guid StudentId { get; set; }
        public Guid TopicId { get; set; }

        public StarTopic(){}
        public StarTopic(Guid studentId, Guid topicId)
        {
            StudentId = studentId;
            TopicId = topicId;
        }
    }
}
