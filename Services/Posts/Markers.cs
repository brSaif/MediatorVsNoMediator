namespace Services.Posts;


public interface IValidatable 
{
}

public interface IUserCommand
{
    string UserId { get; set; }
}