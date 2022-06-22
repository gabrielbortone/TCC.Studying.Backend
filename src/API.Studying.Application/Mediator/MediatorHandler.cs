using API.Studying.Application.Commands;
using API.Studying.Application.Events;
using FluentValidation.Results;
using MediatR;
using System.Threading.Tasks;

namespace API.Studying.Application.Mediator
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ValidationResult> SendCommand<T>(T comando) where T : CommandBase
        {
            return (ValidationResult)await _mediator.Send(comando);
        }

        public async Task<object> SendCommandDiferente<T>(T comando)
        {
            return await _mediator.Send(comando);
        }
    }
}
