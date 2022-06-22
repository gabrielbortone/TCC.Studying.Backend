using API.Studying.Domain.Entities;
using API.Studying.Domain.ViewModel;

namespace API.Studying.Application.DTOs.Reports
{
    public class DocumentViewDto : ViewReportResultDto
    {
        public DocumentViewModel Document { get; set; }
    }
}
