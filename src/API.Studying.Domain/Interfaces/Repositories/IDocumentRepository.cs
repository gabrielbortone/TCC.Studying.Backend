using API.Studying.Domain.Entities;
using API.Studying.Domain.ViewModel;
using System;
using System.Collections.Generic;

namespace API.Studying.Domain.Interfaces.Repositories
{
    public interface IDocumentRepository : IRepositoryBase<Document>
    {
        Document GetById(Guid id);
        List<DocumentViewModel> GetAll();
        List<DocumentViewModel> GetByTopic(Guid topicId);
        List<DocumentViewModel> GetByStudent(Guid studentId);
        List<DocumentViewModel> GetBySearch(string key);
        List<DocumentViewModel> GetByStars();
        void UpdateStars(Guid id, int star);
        void UpdateViews(Document document);
        void DeleteDocument(Guid documentId);
    }
}
