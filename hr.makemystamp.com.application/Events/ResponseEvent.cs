using MediatR;

namespace hr.makemystamp.com.core.Events
{
    public record ResponseEvent(ResponseDto Response) : INotification;
}
