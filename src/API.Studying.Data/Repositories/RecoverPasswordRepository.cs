using API.Studying.Data.DbContext;
using API.Studying.Domain.Entities;
using API.Studying.Domain.Interfaces.Repositories;
using System;
using System.Linq;

namespace API.Studying.Data.Repositories
{
    public class RecoverPasswordRepository : IRecoverPasswordRepository
    {
        private readonly AppDbContext _dbContext;
        public RecoverPasswordRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Create(RecoverPassword entity)
        {
            _dbContext.RecoverPassword.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Delete(Guid id, Guid studentId)
        {
            var recoverPassword = GetById(id);
            _dbContext.RecoverPassword.Remove(recoverPassword);
        }

        public RecoverPassword GetByEmail(string email)
        {
            return _dbContext.RecoverPassword.AsQueryable().OrderBy(r => r.Date).LastOrDefault(r => r.Email.Equals(email));
        }

        public RecoverPassword GetById(Guid id)
        {
            return _dbContext.RecoverPassword.AsQueryable().FirstOrDefault(r => r.Id.Equals(id));
        }

        public void Update(RecoverPassword entity)
        {
            _dbContext.RecoverPassword.Update(entity);
        }
    }
}
