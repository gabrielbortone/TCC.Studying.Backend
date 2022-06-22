using API.Studying.Application.Commands;
using FluentValidation.Results;
using System.Threading.Tasks;

namespace API.Studying.Application.Mediator
{
    public interface IMediatorHandler
    {
        Task<ValidationResult> SendCommand<T>(T comando) where T : CommandBase;
        Task<object> SendCommandDiferente<T>(T comando);
    }
}
