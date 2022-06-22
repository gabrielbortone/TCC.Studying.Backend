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
    public class DocumentRepository : IDocumentRepository
    {
        private readonly AppDbContext _dbContext;

        public DocumentRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Create(Document entity)
        {
            _dbContext.Document.Add(entity);
        }

        private bool IsFromAnotherStudent(Document document, Guid studentId)
        {
            return !document.Student.Id.Equals(studentId);
        }

        public void Delete(Guid id, Guid studentId)
        {
            var document = GetById(id);

            if (document == null)
            {
                throw new ArgumentException("Não foi possível encontrar documento com esse id");
            }
            else
            {
                if (IsFromAnotherStudent(document, studentId))
                {
                    throw new ArgumentException("Impossível deletar dados de outro usuário!");
                }
                else
                {
                    var documentsFavorite = _dbContext.StudentDocument.Where(x => x.DocumentId.Equals(id));
                    _dbContext.StudentDocument.RemoveRange(documentsFavorite);

                    var documentsViewReports = _dbContext.DocumentViewReport.Where(x => x.Document.Id.Equals(id));
                    _dbContext.DocumentViewReport.RemoveRange(documentsViewReports);

                    var documentsStars = _dbContext.StarDocument.Where(x => x.DocumentId.Equals(id));
                    _dbContext.StarDocument.RemoveRange(documentsStars);

                    _dbContext.Document.Remove(document);
                }
            }
        }

        public void DeleteDocument(Guid documentId)
        {
            var document = GetById(documentId);
            if (document == null)
            {
                throw new ArgumentException("Não foi possível deletar um documento com esse id!");
            }

            var documentsFavorite = _dbContext.StudentDocument.Where(x => x.DocumentId.Equals(documentId));
            _dbContext.StudentDocument.RemoveRange(documentsFavorite);

            var documentsViewReports = _dbContext.DocumentViewReport.Where(x => x.Document.Id.Equals(documentId));
            _dbContext.DocumentViewReport.RemoveRange(documentsViewReports);

            var documentsStars = _dbContext.StarDocument.Where(x => x.DocumentId.Equals(documentId));
            _dbContext.StarDocument.RemoveRange(documentsStars);

            _dbContext.Document.Remove(document);
        }

        public List<DocumentViewModel> GetAll()
        {
            return _dbContext.Document
                .Include(d => d.Student)
                .Include(d => d.Topic)
                .AsQueryable()
                .Select(d => new DocumentViewModel(d.Id, d.Title, d.UrlDocument, d.Stars,
                    d.Keys, d.Views, new TopicViewModel(d.Topic.Id, d.Topic.Title, d.Topic.Description),
                    new StudentViewModel(d.Student.Id, d.Student.Name.FirstName, d.Student.Name.LastName, d.Student.UrlImage,
                    d.Student.IsDeleted, d.Student.IsBlocked)))
                .ToList();
        }

        public Document GetById(Guid id)
        {
            return _dbContext.Document
                .Include(d => d.Student)
                .Include(d => d.Topic)
                .AsQueryable()
                .FirstOrDefault(d => d.Id.Equals(id));
        }

        public List<DocumentViewModel> GetBySearch(string key)
        {
            return _dbContext.Document
                .Include(d => d.Student)
                .Include(d => d.Topic)
                .AsQueryable()
                .Where(d => d.Keys.ToLower().Contains(key) || d.Title.ToLower().Contains(key) || d.Topic.Title.ToLower().Contains(key))
                .Select(d => new DocumentViewModel(d.Id, d.Title, d.UrlDocument,d.Stars,
                    d.Keys, d.Views, new TopicViewModel(d.Topic.Id,d.Topic.Title, d.Topic.Description),
                    new StudentViewModel(d.Student.Id, d.Student.Name.FirstName, d.Student.Name.LastName,d.Student.UrlImage,
                    d.Student.IsDeleted, d.Student.IsBlocked)))
                .ToList();
        }

        public List<DocumentViewModel> GetByStudent(Guid studentId)
        {
            return _dbContext.Document
                .Include(d => d.Student)
                .Include(d=> d.Topic)
                .AsQueryable()
                .Where(d => d.Student.Id.Equals(studentId))
                                .Select(d => new DocumentViewModel(d.Id, d.Title, d.UrlDocument, d.Stars,
                    d.Keys, d.Views, new TopicViewModel(d.Topic.Id, d.Topic.Title, d.Topic.Description),
                    new StudentViewModel(d.Student.Id, d.Student.Name.FirstName, d.Student.Name.LastName, d.Student.UrlImage,
                    d.Student.IsDeleted, d.Student.IsBlocked)))
                .OrderBy(d => d.Views)
                .ToList();
        }

        public List<DocumentViewModel> GetByTopic(Guid topicId)
        {
            return _dbContext.Document
                .Include(d => d.Student)
                .Include(d => d.Topic)
                .AsQueryable()
                .Where(d => d.Topic.Id.Equals(topicId))
                                .Select(d => new DocumentViewModel(d.Id, d.Title, d.UrlDocument, d.Stars,
                    d.Keys, d.Views, new TopicViewModel(d.Topic.Id, d.Topic.Title, d.Topic.Description),
                    new StudentViewModel(d.Student.Id, d.Student.Name.FirstName, d.Student.Name.LastName, d.Student.UrlImage,
                    d.Student.IsDeleted, d.Student.IsBlocked)))
                .OrderBy(d=>d.Views)
                .ToList();
        }

        public void Update(Document entity)
        {
            _dbContext.Document.Update(entity);
        }

        public void UpdateStars(Guid id, int star)
        {
            var document = GetById(id);
            document.UpdateStars(star);

            var student = _dbContext.Student.FirstOrDefault(s => s.Id.Equals(document.Student.Id));
            student.UpdatePoints(star);
            _dbContext.Student.Update(student);

            Update(document);
        }

        public void UpdateViews(Document document)
        {
            document.IncreaseView();

            var student = _dbContext.Student.FirstOrDefault(s => s.Id.Equals(document.Student.Id));
            student.UpdatePoints(1);
            _dbContext.Student.Update(student);

            _dbContext.Document.Update(document);
            _dbContext.SaveChanges();
        }

        public List<DocumentViewModel> GetByStars()
        {
            return _dbContext.Document
                .Include(d => d.Student)
                .Include(d => d.Topic)
                .AsEnumerable()
                .Select(d => new DocumentViewModel(d.Id, d.Title, d.UrlDocument, d.Stars,
                    d.Keys, d.Views, new TopicViewModel(d.Topic.Id, d.Topic.Title, d.Topic.Description),
                    new StudentViewModel(d.Student.Id, d.Student.Name.FirstName, d.Student.Name.LastName, d.Student.UrlImage,
                    d.Student.IsDeleted, d.Student.IsBlocked)))
                .OrderBy(d=> d.Stars)
                .ToList();
        }
    }
}
