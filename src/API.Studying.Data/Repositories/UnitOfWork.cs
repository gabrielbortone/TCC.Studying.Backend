using API.Studying.Data.DbContext;
using API.Studying.Domain.Interfaces.Repositories;

namespace API.Studying.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        public IDocumentRepository DocumentRepository { get; set; }
        public IStudentRepository StudentRepository { get; set; }
        public ITopicRepository TopicRepository { get; set; }
        public IVideoRepository VideoRepository { get; set; }
        public IFavoriteRepository FavoriteRepository { get; set; }
        public IStarRepository StarRepository { get; set; }
        public IDocumentViewReportRepository DocumentViewReportRepository { get; set; }
        public IVideoViewReportRepository VideoViewReportRepository { get; set; }
        
        public UnitOfWork(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
            DocumentRepository = new DocumentRepository(_dbContext);
            StudentRepository = new StudentRepository(_dbContext);
            TopicRepository = new TopicRepository(_dbContext);
            VideoRepository = new VideoRepository(_dbContext);
            FavoriteRepository = new FavoriteRepository(_dbContext);
            StarRepository = new StarRepository(_dbContext);
            DocumentViewReportRepository = new DocumentViewReportRepository(_dbContext);
            VideoViewReportRepository = new VideoViewReportRepository(_dbContext);
        }

        public bool Commit()
        {
            return _dbContext.SaveChanges() > 0;
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
