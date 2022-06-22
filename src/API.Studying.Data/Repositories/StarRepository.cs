using API.Studying.Data.DbContext;
using API.Studying.Domain.Entities.PreferencesUser.Star;
using API.Studying.Domain.Interfaces.Repositories;
using System;
using System.Linq;

namespace API.Studying.Data.Repositories
{
    public class StarRepository : IStarRepository
    {
        private readonly AppDbContext _dbContext;

        public StarRepository(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }

        public void CancelStarDocument(Guid studentId, Guid documentId)
        {
            var starDocument = _dbContext.StarDocument.FirstOrDefault(s => s.StudentId.Equals(studentId) && s.DocumentId.Equals(documentId));

            if (starDocument == null)
                throw new ArgumentException("Dado inexistente!");

            _dbContext.StarDocument.Remove(starDocument);
        }

        public void CancelStarTopic(Guid studentId, Guid topicId)
        {
            var starTopic = _dbContext.StarTopic.FirstOrDefault(s => s.StudentId.Equals(studentId) && s.TopicId.Equals(topicId));

            if (starTopic == null)
                throw new ArgumentException("Dado inexistente!");

            _dbContext.StarTopic.Remove(starTopic);
        }

        public void CancelStarVideo(Guid studentId, Guid videoId)
        {
            var starVideo = _dbContext.StarVideo.FirstOrDefault(s => s.StudentId.Equals(studentId) && s.VideoId.Equals(videoId));

            if (starVideo == null)
                throw new ArgumentException("Dado inexistente!");

            _dbContext.StarVideo.Remove(starVideo);
        }

        public StarDocument GetDocument(Guid studentId, Guid documentId)
        {
            return _dbContext.StarDocument.AsQueryable().FirstOrDefault(s => s.StudentId.Equals(studentId) && s.DocumentId.Equals(documentId));

        }

        public StarVideo GetVideo(Guid studentId, Guid videoId)
        {
            return _dbContext.StarVideo.AsQueryable().FirstOrDefault(s => s.StudentId.Equals(studentId) && s.VideoId.Equals(videoId));
        }

        public void StarDocument(Guid studentId, Guid documentId)
        {
            var starDocument = _dbContext.StarDocument.AsQueryable().FirstOrDefault(s => s.StudentId.Equals(studentId) && s.DocumentId.Equals(documentId));
            if (starDocument != null)
                throw new Exception("Já existe no banco de dados!");

            _dbContext.StarDocument.Add(new StarDocument() { StudentId = studentId, DocumentId = documentId });
        }

        public void StarTopic(Guid studentId, Guid topicId)
        {
            var starTopic = _dbContext.StarTopic.AsQueryable().FirstOrDefault(s => s.StudentId.Equals(studentId) && s.TopicId.Equals(topicId));
            if (starTopic != null)
                throw new Exception("Já existe no banco de dados!");

            _dbContext.StarTopic.Add(new StarTopic() { StudentId = studentId, TopicId = topicId});
        }

        public void StarVideo(Guid studentId, Guid videoId)
        {
            var starVideo = _dbContext.StarVideo.AsQueryable().FirstOrDefault(s => s.StudentId.Equals(studentId) && s.VideoId.Equals(videoId));
            if (starVideo != null)
                throw new Exception("Já existe no banco de dados!");

            _dbContext.StarVideo.Add(new StarVideo() { StudentId = studentId, VideoId = videoId});
        }
    }
}
