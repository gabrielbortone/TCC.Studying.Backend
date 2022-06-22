using System;

namespace API.Studying.Application.Events
{
    public abstract class EventBase : MessageBase
    {
        public DateTime Timestamp { get; private set; }
        protected EventBase()
        {
            Timestamp = DateTime.Now;
        }
    }
}
