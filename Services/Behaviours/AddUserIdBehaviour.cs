using MediatR;
using Services.Posts;

namespace Services.Behaviours;

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