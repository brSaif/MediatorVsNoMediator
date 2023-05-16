

using Benchos;
using FluentValidation;

public record CreatePost(string Title, string Body) : IUserCommand
{
    public string UserId { get; set; }

    public class Validator : AbstractValidator<CreatePost>
    {
        public Validator()
        {
            RuleFor(c => c.Title).NotEmpty();
            RuleFor(c => c.Body).NotEmpty();
            RuleFor(c => c.UserId).NotEmpty();
        }
    }
}


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
        
       if( _serviceProvider.GetService(typeof(IValidator<TRequest>)) is IValidator<TRequest> _validator)
       {
           await _validator.ValidateAndThrowAsync(request, cancellationToken);
       }

        var result = await handler(request, cancellationToken);
        return result;
    }
}

public sealed class CreatePostHandler
{
    public Task<bool> Handle(CreatePost request, CancellationToken cancellationToken)
    {
        return Task.FromResult(true);
    }
}