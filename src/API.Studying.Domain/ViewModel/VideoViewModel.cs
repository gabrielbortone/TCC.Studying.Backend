using System;

namespace API.Studying.Domain.ViewModel
{
    public class VideoViewModel
    {
        public Guid Id { get; private set; }
        public TopicViewModel Topic { get; private set; }
        public int Order { get; private set; }
        public string Title { get; private set; }
        public string UrlVideo { get; private set; }
        public int Star { get; private set; }
        public string Keys { get; private set; }
        public int Views { get; private set; } = 0;
        public string Thumbnail { get; private set; }
        public StudentViewModel Student { get; private set; }
        public bool IsFavorite { get; set; }
        public bool IsStar { get; set; }
        public VideoViewModel(Guid id, TopicViewModel topic, int order, string title, string urlVideo, int star, string keys, int views, string thumbnail, StudentViewModel student)
        {
            Id = id;
            Topic = topic;
            Order = order;
            Title = title;
            UrlVideo = urlVideo;
            Star = star;
            Keys = keys;
            Views = views;
            Thumbnail = thumbnail;
            Student = student;
        }
    }
}
