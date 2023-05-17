using MediatR;

namespace Services.Posts;

public class Ping : INotification
{
    public string Message { get; set; }
}

public sealed class PingEventHandlerOne : INotificationHandler<Ping>
{
    public Task Handle(Ping notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Hello from 1st ping handler \n {notification.Message}");
        return Task.CompletedTask;
    }
}

public sealed class PingEventHandlerTwo : INotificationHandler<Ping>
{
    public Task Handle(Ping notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Hello from 2nd ping handler \n {notification.Message}");
        return Task.CompletedTask;
    }
}