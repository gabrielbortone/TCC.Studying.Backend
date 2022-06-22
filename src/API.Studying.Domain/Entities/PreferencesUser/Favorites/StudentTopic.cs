using System;

namespace API.Studying.Domain.Entities.PreferencesUser
{
    public class StudentTopic : Entity
    {
        public Guid StudentId { get; set; }
        public Guid TopicId { get; set; }
        public StudentTopic(){}
        public StudentTopic(Guid studentId, Guid topicId)
        {
            StudentId = studentId;
            TopicId = topicId;
        }
    }
}
