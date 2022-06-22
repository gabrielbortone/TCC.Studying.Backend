using API.Studying.Data.DbContext;
using API.Studying.Domain.Entities.Reports;
using API.Studying.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API.Studying.Data.Repositories
{
    public class VideoViewReportRepository : IVideoViewReportRepository
    {
        private readonly AppDbContext _dbContext;

        public VideoViewReportRepository(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }
        public void Create(VideoViewReport entity)
        {
            _dbContext.VideoViewReport.Add(entity);
            _dbContext.SaveChanges();
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
                if (video.Student.Id.Equals(studentId))
                {
                    _dbContext.VideoViewReport.Remove(video);
                }
                else
                {
                    throw new ArgumentException("Impossível deletar dados de outro usuário!");
                }
            }
        }

        private List<VideoViewReport> GetAllAsc(DateTime begin, DateTime end)
        {
            return _dbContext.VideoViewReport
                .Include(e => e.Student)
                .Include(e => e.Video)
                .ThenInclude(v => v.Topic)
                .Include(e => e.Video)
                .ThenInclude(v => v.Student)
                .AsQueryable()
                .Where(c => c.Date.CompareTo(begin) >= 0 && c.Date.CompareTo(end) <= 0)
                .OrderBy(c => c.Video.Views)
                .ToList();
        }

        private List<VideoViewReport> GetAllDesc(DateTime begin, DateTime end)
        {
            return _dbContext.VideoViewReport
                .Include(e => e.Student)
                .Include(e => e.Video)
                .ThenInclude(v=> v.Topic)
                .Include(e => e.Video)
                .ThenInclude(v=> v.Student)
                .AsQueryable()
                .Where(c => c.Date.CompareTo(begin) >= 0 && c.Date.CompareTo(end) <= 0)
                .OrderByDescending(c => c.Video.Views)
                .ToList();
        }

        public List<VideoViewReport> GetAll(DateTime begin, DateTime end, bool top)
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

        public VideoViewReport GetById(Guid id)
        {
            return _dbContext.VideoViewReport
                .Include(v=> v.Video)
                .Include(v=> v.Student)
                .FirstOrDefault(v=> v.Id.Equals(id));
        }

        public void Update(VideoViewReport entity)
        {
            _dbContext.VideoViewReport.Update(entity);
            _dbContext.SaveChanges();
        }
    }
}
