using API.Studying.Domain.Entities;
using System;
using System.Collections.Generic;

namespace API.Studying.Domain.Interfaces.Repositories
{
    public interface ITopicRepository : IRepositoryBase<Topic>
    {
        Topic GetById(Guid id);
        List<Topic> GetAll();
        List<Topic> GetAllSons(Guid fatherId);
        List<Topic> GetBySearch(string key);
        List<Topic> GetByStudentId(Guid studentId);
    }
}
