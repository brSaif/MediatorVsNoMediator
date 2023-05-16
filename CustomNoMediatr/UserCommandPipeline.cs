using CustomNoMediatr.Posts;
using FluentValidation;

namespace CustomNoMediatr;

public class UserCommandPipeline
{
    private readonly IServiceProvider _serviceProvider;

    public UserCommandPipeline(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<TResponse> Pipe<TRequest, TResponse>(TRequest request,
        Func<TRequest, CancellationToken, Task<TResponse>> handler,
        CancellationToken cancellationToken = default)
    where TRequest : IUserCommand
    {
        request.UserId = "user_id";
        var _validator = _serviceProvider.GetService<IValidator<TRequest>>();
        if (_validator is not null)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);
        }

        var result = await handler(request, cancellationToken);
        return result;
    }
}