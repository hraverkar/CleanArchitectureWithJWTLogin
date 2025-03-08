using hr.makemystamp.com.core.Events;
using MediatR;

namespace hr.makemystamp.com.application.Events
{
    public record ErrorEvent(ResponseDto Response) : INotification;
}
