using API.Studying.Application.DTOs;
using API.Studying.Application.DTOs.Reports;
using API.Studying.Domain.ViewModel;
using System.Collections.Generic;

namespace API.Studying.Application.Services.Interfaces
{
    public interface IReportService
    {
        List<StudentDto> GetRakingofStudents(StudentRankingDto rankingDto);
        List<DocumentViewDto> GetDocumentViewReport(ReportViewDto reportView);
        List<VideoViewDto> GetVideoViewReport(ReportViewDto reportView);
        List<DocumentStarsCountViewModel> GetDocumentStarsCountReport();
        List<VideoStarsCountViewModel> GetVideoStarsCountReport();
        List<MostFavoriteDocumentsViewModel> GetMostFavoriteDocumentsReport();
        List<MostFavoriteVideosViewModel> GetMostFavoriteVideosReport();

    }
}
