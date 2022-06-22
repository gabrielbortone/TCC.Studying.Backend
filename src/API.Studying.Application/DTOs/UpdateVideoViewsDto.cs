using System;
using System.Text.Json.Serialization;

namespace API.Studying.Application.DTOs
{
    public class UpdateVideoViewsDto
    {
        public Guid VideoId { get; set; }

        [JsonConstructor]
        public UpdateVideoViewsDto(Guid videoId)
        {
            VideoId = videoId;
        }
    }
}
