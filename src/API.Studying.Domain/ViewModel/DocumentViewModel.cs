using System;

namespace API.Studying.Domain.ViewModel
{
    public class DocumentViewModel
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string UrlDocument { get; private set; }
        public int Stars { get; private set; }
        public string Keys { get; private set; }
        public int Views { get; private set; }
        public TopicViewModel Topic { get; private set; }
        public StudentViewModel Student { get; private set; }
        public bool IsFavorite { get; set; }
        public bool IsStar { get; set; }
        public DocumentViewModel(Guid id, string title, string urlDocument, int stars, string keys, int views, TopicViewModel topic, StudentViewModel student)
        {
            Id = id;
            Title = title;
            UrlDocument = urlDocument;
            Stars = stars;
            Keys = keys;
            Views = views;
            Topic = topic;
            Student = student;
        }
    }
}
