using System;
using System.Text.Json.Serialization;

namespace API.Studying.Application.Commands.Video
{
    public class CancelStarVideoCommand : CommandBase
    {
        public Guid StudentId { get; set; }
        public Guid VideoId { get; set; }
        [JsonConstructor]
        public CancelStarVideoCommand(Guid studentId, Guid videoId)
        {
            StudentId = studentId;
            VideoId = videoId;
        }
    }
}
