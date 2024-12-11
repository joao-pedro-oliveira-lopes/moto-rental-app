using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotoRentalApp.Application.Interfaces.Messaging
{
    public interface IEventPublisher
    {
        Task Publish<T>(T @event) where T : class;
    }
}