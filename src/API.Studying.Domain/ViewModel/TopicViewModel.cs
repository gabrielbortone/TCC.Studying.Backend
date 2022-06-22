using System;

namespace API.Studying.Domain.ViewModel
{
    public class TopicViewModel
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public TopicViewModel(Guid id, string title, string description)
        {
            Id = id;
            Title = title;
            Description = description;
        }
    }
}
