using API.Studying.Domain.Entities.Reports;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Studying.Domain.Entities
{
    public class Video : Entity
    {
        public Topic Topic { get; private set; }
        public int Order { get; private set; }
        public string Title { get; private set; }
        public string UrlVideo { get; private set; }
        public int Star { get; private set; }
        public string Keys { get; private set; }
        public int Views { get; private set; } = 0;
        public string Thumbnail { get; private set; }
        public Student Student { get; private set; }
        public ICollection<VideoViewReport> VideoViewReports { get; set; }

        [NotMapped]
        public bool IsFavorite { get; set; }

        public Video(){}
        public Video(Topic topic, int order, string title, string urlVideo, 
            int star, string keys, string thumbnail, Student student, ICollection<VideoViewReport> videoViewReports)
        {
            Topic = topic;
            Order = order;
            Title = title;
            UrlVideo = urlVideo;
            Star = star;
            Keys = keys;
            Thumbnail = thumbnail;
            Student = student;
            VideoViewReports = videoViewReports;
        }
        public void IncreaseViews()
        {
            Views++;
        }
        public void UpdateStars(int star)
        {
            Star += star;
            if (Star <= 0)
                Star = 0;
        }

        public void UpdateParams(Topic topic, int order, string title, string urlVideo, string keys, string thumbnail)
        {
            Topic = topic;
            Order = order;
            Title = title;
            UrlVideo = urlVideo;
            Keys = keys;
            Thumbnail = thumbnail;
        }

        public override bool IsValid()
        {
            var validator = new VideoValidator().Validate(this);
            return validator.IsValid;
        }

    }

    public class VideoValidator : AbstractValidator<Video>
    {
        public VideoValidator()
        {
            RuleFor(c => c.Title)
                .MaximumLength(125)
                .WithMessage("Title should be less than 125");

            RuleFor(c => c.Title)
                .NotEqual(string.Empty).NotNull()
                .WithMessage("Title is required");

            RuleFor(c => c.UrlVideo)
                .MaximumLength(250)
                .WithMessage("UrlVideo should be less than 500");

            RuleFor(c => c.UrlVideo)
                .NotEqual(string.Empty).NotNull()
                .WithMessage("UrlVideo is required");
        }
    }
}
