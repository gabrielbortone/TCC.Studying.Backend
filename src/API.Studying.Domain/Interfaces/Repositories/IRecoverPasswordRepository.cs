using API.Studying.Domain.Entities;

namespace API.Studying.Domain.Interfaces.Repositories
{
    public interface IRecoverPasswordRepository : IRepositoryBase<RecoverPassword>
    {
        RecoverPassword GetByEmail(string email);
    }
}
