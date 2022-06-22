using API.Studying.Data.DbContext;
using API.Studying.Domain.Entities;
using API.Studying.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API.Studying.Data.Repositories
{
    public class TopicRepository : ITopicRepository
    {
        private readonly AppDbContext _dbContext;

        public TopicRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Create(Topic entity)
        {
            _dbContext.Topic.Add(entity);
        }

        public void Delete(Guid id, Guid studentId)
        {
            var topic = GetById(id);
            if (topic == null)
            {
                throw new ArgumentException("Não foi possível encontrar um tópico com esse id");
            }
            else
            {
                topic.DeleteTopic();
                _dbContext.Topic.Update(topic);
            }
        }

        public List<Topic> GetAll()
        {
            var allFathers = _dbContext.Topic
                .Include(t => t.FatherTopic)
                .AsQueryable()
                .Where(t => t.FatherTopic == null && !t.IsDeleted)
                .ToList();

            foreach (var father in allFathers)
            {
                father.Sons = GetSonsofSons(father);
            }

            return allFathers;
        }

        private List<Topic> GetSonsofSons(Topic father)
        {
            var sons = new List<Topic>();
            sons = GetAllSons(father.Id);

            foreach (var son in sons)
            {
                son.Sons = GetSons(son).Sons;
            }

            return sons;
        }

        private Topic GetSons(Topic father)
        {
            father.Sons = GetAllSons(father.Id);
            foreach (var son in father.Sons)
            {
                do
                {
                    son.Sons = GetSons(son).Sons;
                } while (son.Sons.Count != 0);
            }

            return father;
        }

        public List<Topic> GetAllSons(Guid fatherId)
        {
            return _dbContext.Topic
                .Where(t=> t.FatherTopicId.Equals(fatherId) && !t.IsDeleted)
                .AsQueryable()
                .ToList();
        }

        public Topic GetById(Guid id)
        {
            return _dbContext.Topic
                .Include(t => t.FatherTopic)
                .Include(t=> t.Documents)
                .Include(t=> t.Videos)
                .AsQueryable()
                .FirstOrDefault(t => t.Id.Equals(id) );
        }

        public List<Topic> GetBySearch(string key)
        {
            var topics =  _dbContext.Topic
                .Include(t => t.FatherTopic)
                .AsQueryable()
                .Where(t => t.Title.ToLower().Contains(key) && !t.IsDeleted)
                .ToList();

            foreach (var father in topics)
            {
                father.Sons = GetSonsofSons(father);
            }

            return topics;
        }

        public List<Topic> GetByStudentId(Guid studentId)
        {
            var student = _dbContext.Student
                .Include(s => s.Documents)
                .ThenInclude(d => d.Topic)
                .Include(s => s.Videos)
                .ThenInclude(p => p.Topic)
                .FirstOrDefault(s=> s.Id.Equals(studentId));

            var topics = new List<Topic>();
            topics.AddRange(student.Documents.Select(d => d.Topic));
            topics.AddRange(student.Videos.Select(p => p.Topic));

            return topics.Distinct().ToList();
        }

        public void Update(Topic entity)
        {
            _dbContext.Topic.Update(entity);
        }
    }
}
