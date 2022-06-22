using FluentValidation.Results;
using MediatR;
using System;

namespace API.Studying.Application.Commands
{
    public abstract class CommandBase : MessageBase, IRequest<ValidationResult>
    {
        public DateTime Timestamp { get; private set; }
        public ValidationResult ValidationResult { get; set; }

        protected CommandBase()
        {
            Timestamp = DateTime.Now;
        }

        public virtual bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}
