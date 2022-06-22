using System;

namespace API.Studying.Domain.Entities.PreferencesUser
{
    public class StudentVideo : Entity
    {
        public Guid StudentId { get; set; }
        public Guid VideoId { get; set; }
        public StudentVideo(){}
        public StudentVideo(Guid studentId, Guid videoId)
        {
            StudentId = studentId;
            VideoId = videoId;
        }
    }
}
