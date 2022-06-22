using API.Studying.Domain.Entities;
using API.Studying.Domain.Entities.PreferencesUser;
using API.Studying.Domain.Entities.PreferencesUser.Favorites;
using API.Studying.Domain.ViewModel;
using System;
using System.Collections.Generic;

namespace API.Studying.Domain.Interfaces.Repositories
{
    public interface IFavoriteRepository
    {
        AllFavoritesViewModel GetAllFavoritesByStudentViewModel(Guid studentId);
        AllFavorites GetAllFavoritesByStudent(Guid studentId);
        StudentDocument GetDocumentByIds(Guid studentId, Guid documentId);
        List<StudentDocument> GetDocumentById(Guid documentId);
        void FavoriteDocument(Guid studentId, Guid documentId);
        void UnfavoriteDocument(Guid studentId, Guid documentId);
        StudentTopic GetTopicByIds(Guid studentId, Guid topicId);
        List<StudentTopic> GetTopicById(Guid topicId);
        void FavoriteTopic(Guid studentId, Guid topicId);
        void UnfavoriteTopic(Guid studentId, Guid topicId);
        StudentVideo GetVideoByIds(Guid studentId, Guid videoId);
        List<StudentVideo> GetVideoById(Guid videoId);
        void FavoriteVideo(Guid studentId, Guid videoId);
        void UnfavoriteVideo(Guid studentId, Guid videoId);
        List<MostFavoriteDocumentsViewModel> GetAllDocumentsFavorites();
        List<MostFavoriteVideosViewModel> GetAllVideosFavorites();
    }
}
