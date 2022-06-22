using System;

namespace API.Studying.Domain.Interfaces.Repositories
{
    public interface IRepositoryBase<T> where T : class
    {
        public void Create(T entity);
        public void Update(T entity);
        public void Delete(Guid id, Guid studentId);
    }
}
