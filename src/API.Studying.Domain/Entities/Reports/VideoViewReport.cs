using System;

namespace API.Studying.Domain.Entities.Reports
{
    public class VideoViewReport : Entity
    {
        public Student Student { get; set; }
        public Video Video { get; set; }
        public DateTime Date { get; set; }
        public VideoViewReport(){}
        public VideoViewReport(Student student, Video video, DateTime date)
        {
            Student = student;
            Video = video;
            Date = date;
        }
    }
}
