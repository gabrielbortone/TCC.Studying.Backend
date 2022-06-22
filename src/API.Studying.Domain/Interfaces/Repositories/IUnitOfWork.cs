using System;

namespace API.Studying.Domain.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IDocumentRepository DocumentRepository { get; set; }
        IStudentRepository StudentRepository { get; set; }
        ITopicRepository TopicRepository { get; set; }
        IVideoRepository VideoRepository { get; set; }
        IFavoriteRepository FavoriteRepository { get; set; }
        IStarRepository StarRepository { get; set; }
        IDocumentViewReportRepository DocumentViewReportRepository { get; set; }
        IVideoViewReportRepository VideoViewReportRepository { get; set; }
        bool Commit();
    }
}
