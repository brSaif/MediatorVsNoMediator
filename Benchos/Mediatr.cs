using FluentValidation;
using MediatR;

namespace Benchos;

public class Post
{
    public int Id { get; set; }
    public string Title { get; set; } = String.Empty;
    public string Body { get; set; } = String.Empty;
    public string CreateBy { get; set; } = String.Empty;

    public Post()
    {
        
    }
}


public class AddUserIdBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IUserCommand
{
    public async Task<TResponse> Handle(TRequest request, 
        RequestHandlerDelegate<TResponse> next, 
        CancellationToken cancellationToken)
    {
        request.UserId = "user_id";
        return await next();
    }
}

public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IValidatable
{
    private readonly IValidator<TRequest> _validator;

    public ValidationBehaviour(IValidator<TRequest> validator)
    {
        _validator = validator;
    }
    
    public async Task<TResponse> Handle(TRequest request, 
        RequestHandlerDelegate<TResponse> next, 
        CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);
        return await next();
    }
}

public record CreatePost(string Title, string Body) : IRequest<bool>, IValidatable, IUserCommand
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

public sealed class CreatePostHandler : IRequestHandler<CreatePost, bool>
{
    public Task<bool> Handle(CreatePost request, CancellationToken cancellationToken)
    {
        return Task.FromResult(true);
    }
}