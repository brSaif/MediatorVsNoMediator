using MediatR;
using Microsoft.Extensions.Logging;

namespace Services.Behaviours;

public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly ILogger<TRequest> _logger;

    public LoggingBehaviour(ILogger<TRequest> logger)
    {
        _logger = logger;
    }
    
    public async Task<TResponse> Handle(TRequest request, 
        RequestHandlerDelegate<TResponse> next, 
        CancellationToken cancellationToken)
    {
        var name = typeof(TRequest).Name;
        
        _logger.LogInformation("Starting execution: '{name}'",name);
        var result = await next();
        _logger.LogInformation("Finished executing '{name}'", name);
        return result;
    }
}