using System;

namespace API.Studying.Application.Commands.Topic
{
    public class UnfavoriteTopicCommand : CommandBase
    {
        public Guid StudentId { get; set; }
        public Guid TopicId { get; set; }
        public UnfavoriteTopicCommand(Guid studentId, Guid topicId)
        {
            StudentId = studentId;
            TopicId = topicId;
        }
    }
}
