using API.Studying.Data.DbContext;
using API.Studying.Domain.Entities;
using API.Studying.Domain.Interfaces.Repositories;
using API.Studying.Domain.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API.Studying.Data.Repositories
{
    public class VideoRepository : IVideoRepository
    {
        private readonly AppDbContext _dbContext;

        public VideoRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Create(Video entity)
        {
            _dbContext.Video.Add(entity);
        }

        private bool IsFromAnotherStudent(Video video, Guid studentId)
        {
            return !video.Student.Id.Equals(studentId);
        }

        public void Delete(Guid id, Guid studentId)
        {
            var video = GetById(id);
            if (video == null)
            {
                throw new ArgumentException("Não foi possível encontrar um vídeo com esse id!");
            }
            else
            {
                if (IsFromAnotherStudent(video, studentId))
                {
                    throw new ArgumentException("Impossível deletar dados de outro usuário!");
                }
                else
                {
                    var videosFavorites = _dbContext.StudentVideo.Where(x => x.VideoId.Equals(id)).ToList();
                    _dbContext.StudentVideo.RemoveRange(videosFavorites);

                    var videoViewReport = _dbContext.VideoViewReport.Where(x => x.Video.Id.Equals(id));
                    _dbContext.VideoViewReport.RemoveRange(videoViewReport);

                    var videoStars = _dbContext.StarVideo.Where(x => x.VideoId.Equals(id));
                    _dbContext.StarVideo.RemoveRange(videoStars);

                    _dbContext.Video.Remove(video);

                }
            }             
        }

        public void DeleteVideo(Guid id)
        {
            var video = GetById(id);

            if(video == null)
            {
                throw new ArgumentException("Não foi possível encontrar vídeo com esse id!");
            }

            var videosFavorites = _dbContext.StudentVideo.Where(x => x.VideoId.Equals(id)).ToList();
            _dbContext.StudentVideo.RemoveRange(videosFavorites);

            var videoViewReport = _dbContext.VideoViewReport.Where(x => x.Video.Id.Equals(id));
            _dbContext.VideoViewReport.RemoveRange(videoViewReport);

            var videoStars = _dbContext.StarVideo.Where(x => x.VideoId.Equals(id));
            _dbContext.StarVideo.RemoveRange(videoStars);

            _dbContext.Video.Remove(video);
        }

        public List<VideoViewModel> GetAll()
        {
            return _dbContext.Video
             .Include(v => v.Student)
             .Include(v => v.Topic)
             .AsQueryable()
             .Select(v => new VideoViewModel(v.Id, new TopicViewModel(v.Topic.Id, v.Topic.Title, v.Topic.Description),
               v.Order, v.Title, v.UrlVideo, v.Star, v.Keys, v.Views, v.Thumbnail,
               new StudentViewModel(v.Student.Id, v.Student.Name.FirstName, v.Student.Name.LastName, v.Student.UrlImage,
               v.Student.IsDeleted, v.Student.IsBlocked)))
             .ToList();
        }

        public Video GetById(Guid id)
        {
            return _dbContext.Video
                .Include(v=> v.Student)
                .Include(v=> v.Topic)
                .AsQueryable()
                .FirstOrDefault(v => v.Id.Equals(id));
        }

        public List<VideoViewModel> GetBySearch(string key)
        {
            return _dbContext.Video
              .Include(v=> v.Student)
              .Include(v=> v.Topic)
              .AsQueryable()
              .Where(v => v.Keys.ToLower().Contains(key) || v.Title.ToLower().Contains(key))
              .Select(v=> new VideoViewModel(v.Id, new TopicViewModel(v.Topic.Id, v.Topic.Title, v.Topic.Description),
                v.Order, v.Title, v.UrlVideo, v.Star, v.Keys, v.Views, v.Thumbnail,
                new StudentViewModel(v.Student.Id, v.Student.Name.FirstName, v.Student.Name.LastName, v.Student.UrlImage,
                v.Student.IsDeleted, v.Student.IsBlocked)))
              .ToList();
        }

        public List<VideoViewModel> GetByTopic(Guid topicId)
        {
            return _dbContext.Video
             .Include(v => v.Student)
             .Include(v => v.Topic)
             .AsQueryable()
             .Where(v => v.Topic.Id.Equals(topicId))
             .Select(v => new VideoViewModel(v.Id, new TopicViewModel(v.Topic.Id, v.Topic.Title, v.Topic.Description),
               v.Order, v.Title, v.UrlVideo, v.Star, v.Keys, v.Views, v.Thumbnail,
               new StudentViewModel(v.Student.Id, v.Student.Name.FirstName, v.Student.Name.LastName, v.Student.UrlImage,
               v.Student.IsDeleted, v.Student.IsBlocked)))
             .ToList();
        }

        public void Update(Video entity)
        {
            _dbContext.Video.Update(entity);
        }

        public void UpdateStars(Guid id, int star)
        {
            var video = GetById(id);
            video.UpdateStars(star);

            var student = _dbContext.Student.FirstOrDefault(s => s.Id.Equals(video.Student.Id));
            student.UpdatePoints(star);
            if (student.Point >= 0)
            {
                _dbContext.Student.Update(student);
            }

            Update(video);
        }

        public void UpdateViews(Video video)
        {
            video.IncreaseViews();
            _dbContext.Video.Update(video);

            var student = _dbContext.Student.FirstOrDefault(s => s.Id.Equals(video.Student.Id));
            student.UpdatePoints(1);
            if (student.Point >= 0)
            {
                _dbContext.Student.Update(student);
            }
            _dbContext.SaveChanges();
        }

        public List<VideoViewModel> GetByStars()
        {
            return _dbContext.Video
              .Include(v => v.Student)
              .Include(v => v.Topic)
              .AsEnumerable()
              .Select(v => new VideoViewModel(v.Id, new TopicViewModel(v.Topic.Id, v.Topic.Title, v.Topic.Description),
                v.Order, v.Title, v.UrlVideo, v.Star, v.Keys, v.Views, v.Thumbnail,
                new StudentViewModel(v.Student.Id, v.Student.Name.FirstName, v.Student.Name.LastName, v.Student.UrlImage, v.Student.IsDeleted, v.Student.IsBlocked)))
              .OrderBy(v=> v.Star)
              .ToList();
        }
    }
}
