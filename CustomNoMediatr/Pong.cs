using CustomNoMediatr.Posts;

namespace CustomNoMediatr;

public class PongHandler
{
    private readonly PongEventOneHandler _pongEventOneHandler;
    private readonly PongEventTwoHandler _pongEventTwoHandler;

    public PongHandler(PongEventOneHandler pongEventOneHandler,
        PongEventTwoHandler pongEventTwoHandler)
    {
        _pongEventOneHandler = pongEventOneHandler;
        _pongEventTwoHandler = pongEventTwoHandler;
    }

    public Task Handle(Pong notification, CancellationToken cancellationToken = default)
    {
        return Task.WhenAll(
            _pongEventOneHandler.Handle(notification, cancellationToken),
            _pongEventTwoHandler.Handle(notification, cancellationToken)
        );
    }
}

public class Pong 
{
    public string Message { get; set; }
}

public sealed class PongEventOneHandler : INotificationHandler<Pong>
{
    private readonly ILogger<PongEventOneHandler> _logger;

    public PongEventOneHandler(ILogger<PongEventOneHandler> logger)
    {
        _logger = logger;
    }
    public Task Handle(Pong notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Hello from 1st ping handler \n {notification.Message}");
        return Task.CompletedTask;
    }
}

public sealed class PongEventTwoHandler : INotificationHandler<Pong>
{
    private readonly ILogger<PongEventTwoHandler> _logger;

    public PongEventTwoHandler(ILogger<PongEventTwoHandler> logger)
    {
        _logger = logger;
    }
    public Task Handle(Pong notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Hello from 2nd ping handler \n {notification.Message}");
        return Task.CompletedTask;
    }
}