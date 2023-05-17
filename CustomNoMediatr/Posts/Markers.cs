using System.Reflection.Metadata;

namespace CustomNoMediatr.Posts;


public interface IValidatable 
{
}

public interface IUserCommand
{
    string UserId { get; set; }
}

interface INotificationHandler<in T>
{
    Task Handle(T notification, CancellationToken cancellationToken = default);
}