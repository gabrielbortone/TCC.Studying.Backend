using API.Studying.Domain.Entities.PreferencesUser.Star;
using System;

namespace API.Studying.Domain.Interfaces.Repositories
{
    public interface IStarRepository
    {
        StarDocument GetDocument(Guid studentId, Guid documentId);
        StarVideo GetVideo(Guid studentId, Guid videoId);
        void StarDocument(Guid studentId, Guid documentId);
        void CancelStarDocument(Guid studentId, Guid documentId);
        void StarTopic(Guid studentId, Guid topicId);
        void CancelStarTopic(Guid studentId, Guid topicId);
        void StarVideo(Guid studentId, Guid videoId);
        void CancelStarVideo(Guid studentId, Guid videoId);
    }
}
