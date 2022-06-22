using System;

namespace API.Studying.Application
{
    public abstract class MessageBase
    {
        public string MessageType { get; protected set; }
        public Guid AggregateId { get; protected set; }

        protected MessageBase()
        {
            MessageType = GetType().Name;
        }
    }
}
