﻿namespace Bioworld.CQRS.Events
{
    using System.Threading.Tasks;

    public interface IEventHandler<in TEvent> where TEvent : class, IEvent
    {
        Task HandleAsync(TEvent @event);
    }
}