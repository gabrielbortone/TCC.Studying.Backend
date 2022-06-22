using System;

namespace API.Studying.Domain.Entities.PreferencesUser.Star
{
    public class StarVideo : Entity
    {
        public Guid StudentId { get; set; }
        public Guid VideoId { get; set; }

        public StarVideo() {}
        public StarVideo(Guid studentId, Guid videoId)
        {
            StudentId = studentId;
            VideoId = videoId;
        }
    }
}
