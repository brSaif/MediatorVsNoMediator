namespace Services.Posts;

public class PingHandler
{
    private readonly PingEventOneHandler _pingEventOneHandler;
    private readonly PingEventTwoHandler _pingEventTwoHandler;

    public PingHandler(PingEventOneHandler pingEventOneHandler,
        PingEventTwoHandler pingEventTwoHandler)
    {
        _pingEventOneHandler = pingEventOneHandler;
        _pingEventTwoHandler = pingEventTwoHandler;
    }

    public Task Handle(Ping notification, CancellationToken cancellationToken = default)
    {
        return Task.WhenAll(
            _pingEventOneHandler.Handle(notification, cancellationToken),
            _pingEventTwoHandler.Handle(notification, cancellationToken)
        );
    }
}

public class Ping 
{
    public string Message { get; set; }
}

public sealed class PingEventOneHandler 
{
    private readonly ILogger<PingEventOneHandler> _logger;

    public PingEventOneHandler(ILogger<PingEventOneHandler> logger)
    {
        _logger = logger;
    }
    
    public Task Handle(Ping notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Hello from 1st ping handler \n {notification.Message}");
        return Task.CompletedTask;
    }
}

public sealed class PingEventTwoHandler 
{
    private readonly ILogger<PingEventTwoHandler> _logger;

    public PingEventTwoHandler(ILogger<PingEventTwoHandler> logger)
    {
        _logger = logger;
    }
    
    public Task Handle(Ping notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Hello from 2nd ping handler \n {notification.Message}");
        return Task.CompletedTask;
    }
}