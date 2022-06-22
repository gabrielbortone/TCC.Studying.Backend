using API.Studying.Application.DTOs;
using API.Studying.Application.DTOs.Reports;
using API.Studying.Application.Services.Interfaces;
using API.Studying.Domain.Interfaces.Repositories;
using API.Studying.Domain.ViewModel;
using System.Collections.Generic;
using System.Linq;

namespace API.Studying.Application.Services
{
    public class ReportService : IReportService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ReportService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<DocumentStarsCountViewModel> GetDocumentStarsCountReport()
        {
            var documents = _unitOfWork.DocumentRepository.GetByStars();
            var listDocumentStarVM = new List<DocumentStarsCountViewModel>();
            var position = 1;
            foreach(var document in documents)
            {
                listDocumentStarVM.Add(new DocumentStarsCountViewModel()
                {
                    Position = position,
                    Document = document
                });
                position++;
            }
            return listDocumentStarVM;
        }

        public List<DocumentViewDto> GetDocumentViewReport(ReportViewDto reportView)
        {
            var documentsViews = _unitOfWork.DocumentViewReportRepository.GetAll(reportView.Begin, reportView.End, reportView.Up);
            var documentsViewsDto = new List<DocumentViewDto>();
            var documentsDistinct = documentsViews.Select(c => c.Document).Distinct().ToList();
            foreach (var document in documentsDistinct)
            {
                var documentViewDto = new DocumentViewDto();
                documentViewDto.Document = new DocumentViewModel(document.Id, document.Title,document.UrlDocument,document.Stars,
                    document.UrlDocument,document.Views, new TopicViewModel(document.Topic.Id, document.Topic.Title,document.Topic.Description),
                    new StudentViewModel(document.Student.Id, document.Student.Name.FirstName, document.Student.Name.LastName,
                    document.Student.UrlImage, document.Student.IsDeleted, document.Student.IsBlocked));
                documentViewDto.Dates = documentsViews.Where(c => c.Document == document).Select(c => c.Date).ToList();
                var students = documentsViews.Where(d => d.Document == document).Select(c => c.Student).ToList();
                var studentsDto = new List<StudentDto>();
                foreach (var student in students)
                {
                    var studentDto = new StudentDto(student.Id, student.Name.FirstName, student.Name.LastName,
                        student.UrlImage, student.IsAdmin, student.Point, student.IdentityUser.Email,
                        student.IdentityUser.UserName, student.IsDeleted, student.IsBlocked);
                    studentsDto.Add(studentDto);
                }
                documentViewDto.Students = studentsDto;
                documentsViewsDto.Add(documentViewDto);
            }

            return documentsViewsDto.ToList();
        }

        public List<MostFavoriteDocumentsViewModel> GetMostFavoriteDocumentsReport()
        {
            return _unitOfWork.FavoriteRepository.GetAllDocumentsFavorites();
        }

        public List<MostFavoriteVideosViewModel> GetMostFavoriteVideosReport()
        {
            return _unitOfWork.FavoriteRepository.GetAllVideosFavorites();
        }

        public List<StudentDto> GetRakingofStudents(StudentRankingDto rankingDto)
        {
            var students = _unitOfWork.StudentRepository.GetRaking(rankingDto.Top);
            var studentsDto = new List<StudentDto>();
            foreach (var student in students)
            {
                studentsDto.Add(
                    new StudentDto(student.Id, student.Name.FirstName, student.Name.LastName, 
                    student.UrlImage, student.IsAdmin, student.Point, student.IdentityUser.Email, 
                    student.IdentityUser.UserName, student.IsDeleted, student.IsBlocked));
            }
            return studentsDto;
        }

        public List<VideoStarsCountViewModel> GetVideoStarsCountReport()
        {
            var videos = _unitOfWork.VideoRepository.GetByStars();
            var listVideoStarVM = new List<VideoStarsCountViewModel>();
            var position = 1;
            foreach(var video in videos)
            {
                listVideoStarVM.Add(new VideoStarsCountViewModel()
                {
                    Position = position,
                    Video = video
                });
                position++;
            }
            return listVideoStarVM;
        }

        public List<VideoViewDto> GetVideoViewReport(ReportViewDto reportView)
        {
            var videosViews = _unitOfWork.VideoViewReportRepository.GetAll(reportView.Begin, reportView.End, reportView.Up);
            var videosViewsDto = new List<VideoViewDto>();
            var videosDistinct = videosViews.Select(v => v.Video).Distinct().ToList();
            foreach (var video in videosDistinct)
            {
                var videoViewDto = new VideoViewDto();
                videoViewDto.Video = new VideoViewModel(video.Id, new TopicViewModel(video.Topic.Id, video.Topic.Title, video.Topic.Description),
                    video.Order, video.Title, video.UrlVideo, video.Star, video.Keys, video.Views, video.Thumbnail,
                    new StudentViewModel(video.Student.Id, video.Student.Name.FirstName, video.Student.Name.LastName,
                    video.Student.UrlImage, video.Student.IsDeleted, video.Student.IsBlocked));
                videoViewDto.Dates = videosViews.Where(p => p.Video == video).Select(c => c.Date).ToList();
                var students = videosViews.Where(p => p.Video == video).Select(p => p.Student).ToList();
                var studentsDto = new List<StudentDto>();
                foreach (var student in students)
                {
                    var studentDto = new StudentDto(student.Id, student.Name.FirstName, student.Name.LastName,
                        student.UrlImage, student.IsAdmin, student.Point, student.IdentityUser.Email,
                        student.IdentityUser.UserName, student.IsDeleted, student.IsBlocked);
                    studentsDto.Add(studentDto);
                }
                videoViewDto.Students = studentsDto;
                videosViewsDto.Add(videoViewDto);
            }

            return videosViewsDto.ToList();
        }
    }
}
