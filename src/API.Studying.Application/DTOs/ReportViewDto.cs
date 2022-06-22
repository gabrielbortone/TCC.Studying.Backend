using System;
using System.Text.Json.Serialization;

namespace API.Studying.Application.DTOs
{
    public class ReportViewDto
    {
        public DateTime Begin { get; set; }
        public DateTime End { get; set; }
        public bool Up { get; set; }

        [JsonConstructor]
        public ReportViewDto(DateTime begin, DateTime end, bool up)
        {
            Begin = begin;
            End = end;
            Up = up;
        }
    }
}
