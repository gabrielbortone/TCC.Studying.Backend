using System;

namespace API.Studying.Application.Commands.Video
{
    public class StarVideoCommand : CommandBase
    {
        public Guid StudentId { get; set; }
        public Guid VideoId { get; set; }
        public StarVideoCommand(Guid studentId, Guid videoId)
        {
            StudentId = studentId;
            VideoId = videoId;
        }
    }
}
