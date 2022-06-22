using System;

namespace API.Studying.Domain.ViewModel
{
    public class DocumentViewModelWithoutAuthor
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string UrlDocument { get; private set; }
        public int Stars { get; private set; }
        public string Keys { get; private set; }
        public int Views { get; private set; }
        public TopicViewModel Topic { get; private set; }
        public DocumentViewModelWithoutAuthor(Guid id, string title, string urlDocument, int stars, string keys, int views, TopicViewModel topic)
        {
            Id = id;
            Title = title;
            UrlDocument = urlDocument;
            Stars = stars;
            Keys = keys;
            Views = views;
            Topic = topic;
        }

    }
}
