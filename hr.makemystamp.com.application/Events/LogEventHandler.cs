using hr.makemystamp.com.core.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hr.makemystamp.com.application.Events
{
    public class LogEventHandler : INotificationHandler<ResponseEvent>, INotificationHandler<ErrorEvent>
    {
        public Task Handle(ResponseEvent notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"User ID: {notification.Response.Id} , Message: {notification.Response.ActionMessage}");
            return Task.CompletedTask;
        }

        public Task Handle(ErrorEvent notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"User ID: {notification.Response.Id} , Message: {notification.Response.ActionMessage}");
            return Task.CompletedTask;
        }
    }
}
