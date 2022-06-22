using API.Studying.Domain.Entities;
using API.Studying.Domain.ViewModel;
using System;
using System.Collections.Generic;

namespace API.Studying.Domain.Interfaces.Repositories
{
    public interface IVideoRepository : IRepositoryBase<Video>
    {
        Video GetById(Guid id);
        void UpdateStars(Guid id, int star);
        void UpdateViews(Video video);
        void DeleteVideo(Guid id);
        List<VideoViewModel> GetBySearch(string key);
        List<VideoViewModel> GetAll();
        List<VideoViewModel> GetByTopic(Guid topicId);
        List<VideoViewModel> GetByStars();
    }
}
