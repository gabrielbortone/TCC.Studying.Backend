using API.Studying.Domain.Interfaces.Repositories;
using FluentValidation.Results;

namespace API.Studying.Application.Handlers
{
    public abstract class CommandHandler
    {
        protected ValidationResult ValidationResult;
        protected bool IsSucessfull;

        protected CommandHandler()
        {
            ValidationResult = new ValidationResult();
        }

        protected void AdicionarErro(string mensagem)
        {
            ValidationResult.Errors.Add(new ValidationFailure(string.Empty, mensagem));
        }

        protected ValidationResult PersistirDados(IUnitOfWork uow)
        {
            if (!uow.Commit()) AdicionarErro("Houve um erro ao persistir os dados");

            return ValidationResult;
        }
    }
}