using API.Studying.Application.Commands.Suport;
using API.Studying.Application.Commands.Suport.Enum;
using API.Studying.Application.Utils.EmailConfig;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace API.Studying.Application.Handlers
{
    public class SuportHandler : CommandHandler,
                                 IRequestHandler<CreateSuportRequestCommand, ValidationResult>
    {
        private readonly ISendEmailService _sendEmailService;

        public SuportHandler(ISendEmailService sendEmailService)
        {
            _sendEmailService = sendEmailService;
        }

        public async Task<ValidationResult> Handle(CreateSuportRequestCommand request, CancellationToken cancellationToken)
        {
            var isValid = request.IsValid();
            ValidationResult = request.ValidationResult;

            if (isValid)
            {
                var text = request.Text;
                var subject = ((TypeOfSuport)request.TypeOfSuport).ToString();
                var result = await _sendEmailService.SendEmailAsync(text, subject, request.Email);
                if (!result)
                {
                    AdicionarErro("Não foi possível enviar o email");
                    return ValidationResult;
                }
            }

            return ValidationResult;
        }
    }
}
