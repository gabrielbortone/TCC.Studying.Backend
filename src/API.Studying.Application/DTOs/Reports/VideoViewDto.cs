using API.Studying.Domain.ViewModel;

namespace API.Studying.Application.DTOs.Reports
{
    public class VideoViewDto : ViewReportResultDto
    {
        public VideoViewModel Video { get; set; }
    }
}
