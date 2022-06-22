using API.Studying.Data.DbContext;
using API.Studying.Domain.Entities;
using API.Studying.Domain.Entities.PreferencesUser;
using API.Studying.Domain.Entities.PreferencesUser.Favorites;
using API.Studying.Domain.Interfaces.Repositories;
using API.Studying.Domain.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API.Studying.Data.Repositories
{
    public class FavoriteRepository : IFavoriteRepository
    {
        private readonly AppDbContext _dbContext;
        public FavoriteRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void FavoriteDocument(Guid studentId, Guid documentId)
        {
            var exists = _dbContext.StudentDocument.FirstOrDefault(f => f.DocumentId.Equals(documentId) && f.StudentId.Equals(studentId)) != null;

            if (!exists)
            {
                _dbContext.StudentDocument.Add(new StudentDocument(studentId, documentId));

                var document = _dbContext.Document.Include(d => d.Student).FirstOrDefault(d => d.Id.Equals(documentId));
                var student = _dbContext.Student.FirstOrDefault(s => s.Id.Equals(document.Student.Id));
                student.UpdatePoints(1);
                if (student.Point >= 0)
                {
                    _dbContext.Student.Update(student);
                }
            }
            else
            {
                throw new ArgumentException("Já existe esse dado no banco!");
            }

            
        }

        public void FavoriteTopic(Guid studentId, Guid topicId)
        {
            var exists = _dbContext.StudentTopic.FirstOrDefault(f => f.TopicId.Equals(topicId) && f.StudentId.Equals(studentId)) != null;
            if (!exists)
            {
                _dbContext.StudentTopic.Add(new StudentTopic(studentId, topicId));
            }
            else
            {
                throw new ArgumentException("Já existe esse dado no banco!");
            }
        }

        public void FavoriteVideo(Guid studentId, Guid videoId)
        {
            var exists = _dbContext.StudentVideo.FirstOrDefault(f => f.VideoId.Equals(videoId) && f.StudentId.Equals(studentId)) != null;

            if (!exists)
            {
                _dbContext.StudentVideo.Add(new StudentVideo(studentId, videoId));
                var video = _dbContext.Video.Include(v => v.Student).FirstOrDefault(v => v.Id.Equals(videoId));
                var student = _dbContext.Student.FirstOrDefault(s => s.Id.Equals(video.Student.Id));
                student.UpdatePoints(1);
                if (student.Point >= 0)
                {
                    _dbContext.Student.Update(student);
                }
            }
            else
            {
                throw new ArgumentException("Esse dado já existe no banco!");
            }
            
        }

        private List<StudentDocument> GetDocumentsFavoritesByStudent(Guid studentId)
        {
            return _dbContext.StudentDocument.Where(d => d.StudentId.Equals(studentId)).ToList();
        }

        private List<StudentTopic> GetTopicsFavoritesByStudent(Guid studentId)
        {
            return _dbContext.StudentTopic.Where(t => t.StudentId.Equals(studentId)).ToList();
        }
        
        private List<StudentVideo> GetVideosFavoritesByStudent(Guid studentId)
        {
            return _dbContext.StudentVideo.Where(v => v.StudentId.Equals(studentId)).ToList();
        }

        private AllFavorites AddDocuments(List<StudentDocument> favoriteDocuments, AllFavorites allFavorites)
        {
            foreach (var favoriteDocument in favoriteDocuments)
            {
                var document = _dbContext.Document
                    .Include(d=> d.Topic)
                    .Include(d=> d.Student)
                    .FirstOrDefault(d=> d.Id.Equals(favoriteDocument.DocumentId));

                if (document != null)
                    allFavorites.Documents.Add(document);
            }

            return allFavorites;
        }

        private AllFavorites AddTopics(List<StudentTopic> favoriteTopics, AllFavorites allFavorites)
        {
            foreach (var favoriteTopic in favoriteTopics)
            {
                var topic = _dbContext.Topic.FirstOrDefault(d => d.Id.Equals(favoriteTopic.TopicId));
                if (topic != null)
                    allFavorites.Topics.Add(topic);
            }

            return allFavorites;
        }

        private AllFavorites AddVideos(List<StudentVideo> favoriteVideos, AllFavorites allFavorites)
        {
            foreach (var favoriteVideo in favoriteVideos)
            {
                var video = _dbContext.Video
                    .Include(v => v.Topic)
                    .Include(v => v.Student)
                    .FirstOrDefault(v => v.Id.Equals(favoriteVideo.VideoId)); ;
                if (video != null)
                    allFavorites.Videos.Add(video);
            }

            return allFavorites;
        }

        public AllFavorites GetAllFavoritesByStudent(Guid studentId)
        {
            var allFavorites = new AllFavorites();
            var favoriteDocuments = GetDocumentsFavoritesByStudent(studentId);
            var favoriteTopics = GetTopicsFavoritesByStudent(studentId);
            var favoriteVideos = GetVideosFavoritesByStudent(studentId);

            allFavorites = AddDocuments(favoriteDocuments, allFavorites);
            allFavorites = AddTopics(favoriteTopics, allFavorites);
            allFavorites = AddVideos(favoriteVideos, allFavorites);

            return allFavorites;
        }

        public List<StudentDocument> GetDocumentById(Guid documentId)
        {
            return _dbContext.StudentDocument.Where(d => d.DocumentId.Equals(documentId)).ToList();
        }

        public StudentDocument GetDocumentByIds(Guid studentId, Guid documentId)
        {
            return _dbContext.StudentDocument.FirstOrDefault(d => d.StudentId.Equals(studentId) && d.DocumentId.Equals(documentId));
        }
        public List<StudentTopic> GetTopicById(Guid topicId)
        {
            return _dbContext.StudentTopic.Where(t=> t.TopicId.Equals(topicId)).ToList();
        }

        public StudentTopic GetTopicByIds(Guid studentId, Guid topicId)
        {
            return _dbContext.StudentTopic.FirstOrDefault(t => t.StudentId.Equals(studentId) && t.TopicId.Equals(topicId));
        }

        public List<StudentVideo> GetVideoById(Guid videoId)
        {
            return _dbContext.StudentVideo.Where(v => v.VideoId.Equals(videoId)).ToList();
        }

        public StudentVideo GetVideoByIds(Guid studentId, Guid videoId)
        {
            return _dbContext.StudentVideo.FirstOrDefault(v => v.StudentId.Equals(studentId) && v.VideoId.Equals(videoId));
        }

        public void UnfavoriteDocument(Guid studentId, Guid documentId)
        {
            var studentDocument = _dbContext.StudentDocument.FirstOrDefault(f => f.StudentId.Equals(studentId) && f.DocumentId.Equals(documentId));
            _dbContext.StudentDocument.Remove(studentDocument);
            var video = _dbContext.Document.Include(d => d.Student).FirstOrDefault(d => d.Id.Equals(documentId));
            var student = _dbContext.Student.FirstOrDefault(s => s.Id.Equals(video.Student.Id));
            student.UpdatePoints(-1);
            if (student.Point >= 0)
            {
                _dbContext.Student.Update(student);
            }
        }
        public void UnfavoriteTopic(Guid studentId, Guid topicId)
        {
            var studentTopic = _dbContext.StudentTopic.FirstOrDefault(f => f.StudentId.Equals(studentId) && f.TopicId.Equals(topicId));
            _dbContext.StudentTopic.Remove(studentTopic);
        }

        public void UnfavoriteVideo(Guid studentId, Guid videoId)
        {
            var studentVideo = _dbContext.StudentVideo.FirstOrDefault(
                f=> f.StudentId.Equals(studentId) && f.VideoId.Equals(videoId));

            _dbContext.StudentVideo.Remove(studentVideo);
            var video = _dbContext.Video.Include(p => p.Student).FirstOrDefault(p => p.Id.Equals(videoId));
            var student = _dbContext.Student.FirstOrDefault(s => s.Id.Equals(video.Student.Id));
            student.UpdatePoints(-1);
            if (student.Point >= 0)
            {
                _dbContext.Student.Update(student);
            }
        }

        private List<Guid> GetAllDocumentIds()
        {
            var listDocumentGuids = _dbContext.StudentDocument
                .Select(f => f.DocumentId)
                .AsEnumerable()
                .Distinct()
                .ToList();

            return listDocumentGuids;
        }

        private DocumentViewModel GetDocumentViewModel(Guid id)
        {
            return _dbContext.Document
                .Include(d => d.Student)
                .Include(d => d.Topic)
                .AsNoTracking()
                .AsEnumerable()
                .Select(d => new DocumentViewModel(d.Id, d.Title, d.UrlDocument, d.Stars,
                    d.Keys, d.Views, new TopicViewModel(d.Topic.Id, d.Topic.Title, d.Topic.Description),
                    new StudentViewModel(d.Student.Id, d.Student.Name.FirstName, d.Student.Name.LastName, d.Student.UrlImage,
                    d.Student.IsDeleted, d.Student.IsBlocked)))
                .FirstOrDefault(d=> d.Id.Equals(id));
        }

        private int GetDocumentCount(Guid documentId)
        {
            return _dbContext.StudentDocument
                .AsNoTracking()
                .AsEnumerable()
                .Where(d => d.DocumentId.Equals(documentId))
                .Count();
        }

        public List<MostFavoriteDocumentsViewModel> GetAllDocumentsFavorites()
        {
            var documentIds = GetAllDocumentIds();
            var list = new List<MostFavoriteDocumentsViewModel>();

            foreach (var documentId in documentIds)
            {
                var document = GetDocumentViewModel(documentId);
                var documentCount = GetDocumentCount(documentId);

                list.Add(new MostFavoriteDocumentsViewModel()
                {
                    Document = document,
                    Count = documentCount
                });
            }

            return list.OrderBy(l => l.Count).ToList();
        }

        private List<Guid> GetAllVideoIds()
        {
            var listVideoGuid = _dbContext.StudentVideo
                .Select(f => f.VideoId)
                .AsEnumerable()
                .Distinct()
                .ToList();

            return listVideoGuid;
        }

        private VideoViewModel GetVideoViewModel(Guid id)
        {
            return _dbContext.Video
             .Include(v => v.Student)
             .Include(v => v.Topic)
             .AsNoTracking()
             .AsEnumerable()
             .Select(v => new VideoViewModel(v.Id, new TopicViewModel(v.Topic.Id, v.Topic.Title, v.Topic.Description),
               v.Order, v.Title, v.UrlVideo, v.Star, v.Keys, v.Views, v.Thumbnail,
               new StudentViewModel(v.Student.Id, v.Student.Name.FirstName, v.Student.Name.LastName, v.Student.UrlImage,
               v.Student.IsDeleted, v.Student.IsBlocked)))
             .FirstOrDefault(v => v.Id.Equals(id));
        }

        private int GetVideoCount(Guid videoId)
        {
            return _dbContext.StudentVideo
                .AsNoTracking()
                .AsEnumerable()
                .Where(v=> v.VideoId.Equals(videoId))
                .Count();
        }

        public List<MostFavoriteVideosViewModel> GetAllVideosFavorites()
        {
            var videoIds = GetAllVideoIds();
            var list = new List<MostFavoriteVideosViewModel>();

            foreach (var videoId in videoIds)
            {
                var video = GetVideoViewModel(videoId);
                var videoCount = GetVideoCount(videoId);

                list.Add(new MostFavoriteVideosViewModel()
                {
                    Video = video,
                    Count = videoCount
                });
            }

            return list.OrderBy(l => l.Count).ToList();
        }

        public AllFavoritesViewModel GetAllFavoritesByStudentViewModel(Guid studentId)
        {
            var allFavorites = GetAllFavoritesByStudent(studentId);
            AllFavoritesViewModel allFavoritesVM = new AllFavoritesViewModel();
            foreach(var video in allFavorites.Videos)
            {
                var videoVM = new VideoViewModel(video.Id, new TopicViewModel(video.Topic.Id, video.Topic.Title, video.Topic.Description),
                    video.Order, video.Title, video.UrlVideo, video.Star, video.Keys, video.Views, video.Thumbnail,
                    new StudentViewModel(video.Student.Id, video.Student.Name.FirstName, video.Student.Name.LastName, video.Student.UrlImage,
                    video.Student.IsDeleted, video.Student.IsBlocked));

                allFavoritesVM.VideosFavorites.Add(videoVM);
            }

            foreach (var document in allFavorites.Documents)
            {
                var documentVM = new DocumentViewModel(document.Id, document.Title, document.UrlDocument,
                    document.Stars, document.Keys, document.Views,
                    new TopicViewModel(document.Topic.Id, document.Topic.Title, document.Topic.Description),
                    new StudentViewModel(document.Student.Id, document.Student.Name.FirstName, document.Student.Name.LastName, document.Student.UrlImage,
                    document.Student.IsDeleted, document.Student.IsBlocked));

                allFavoritesVM.DocumentsFavorites.Add(documentVM);
            }

            foreach (var topic in allFavorites.Topics)
            {
                var topicVM = new TopicViewModel(topic.Id, topic.Title, topic.Description);
                allFavoritesVM.TopicsFavorites.Add(topicVM);
            }

            return allFavoritesVM;
        }
    }
}
