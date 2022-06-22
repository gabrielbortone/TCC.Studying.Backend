using API.Studying.Data.DbContext;
using API.Studying.Domain.Entities.Reports;
using API.Studying.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API.Studying.Data.Repositories
{
    public class DocumentViewReportRepository : IDocumentViewReportRepository
    {
        private readonly AppDbContext _dbContext;
        public DocumentViewReportRepository(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }
        public void Create(DocumentViewReport entity)
        {
            _dbContext.DocumentViewReport.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Delete(Guid id, Guid studentId)
        {
            var documentViewReport = GetById(id);
            _dbContext.DocumentViewReport.Remove(documentViewReport);
        }

        private List<DocumentViewReport> GetAllAsc(DateTime begin, DateTime end)
        {
            return _dbContext.DocumentViewReport
                .Include(c => c.Student)
                .Include(c => c.Document)
                .ThenInclude(d => d.Topic)
                .Include(c => c.Document)
                .ThenInclude(d => d.Student)
                .AsQueryable()
                .Where(c => c.Date.CompareTo(begin) >= 0 && c.Date.CompareTo(end) <= 0)
                .OrderBy(c => c.Document.Views)
                .ToList();
        }

        private List<DocumentViewReport> GetAllDesc(DateTime begin, DateTime end)
        {
            return _dbContext.DocumentViewReport
               .Include(c => c.Student)
               .Include(c => c.Document)
               .ThenInclude(d=> d.Topic)
               .Include(c => c.Document)
               .ThenInclude(d => d.Student)
               .AsQueryable()
               .Where(c => c.Date.CompareTo(begin) >= 0 && c.Date.CompareTo(end) <= 0)
               .OrderByDescending(c => c.Document.Views)
               .ToList();
        }

        public List<DocumentViewReport> GetAll(DateTime begin, DateTime end, bool top)
        {
            if (top)
            {
                return GetAllAsc(begin, end);
            }
            else
            {
                return GetAllDesc(begin, end);
            }
        }

        public DocumentViewReport GetById(Guid id)
        {
            return _dbContext.DocumentViewReport.FirstOrDefault(r => r.Id.Equals(id));
        }

        public void Update(DocumentViewReport entity)
        {
            _dbContext.DocumentViewReport.Update(entity);
        }
    }
}
