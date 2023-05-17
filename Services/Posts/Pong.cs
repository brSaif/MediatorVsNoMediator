using MediatR;

namespace Services.Posts;

public class Pong : INotification
{
    public string Message { get; set; }
}

public sealed class PongEventHandlerOne : INotificationHandler<Pong>
{
    public Task Handle(Pong notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Hello from 1st pong handler {notification.Message}");
        return Task.CompletedTask;
    }
}

public sealed class PongEventHandlerTwo : INotificationHandler<Pong>
{
    public Task Handle(Pong notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Hello from 2nd pong handler {notification.Message}");
        return Task.CompletedTask;
    }
}