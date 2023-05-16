using FluentValidation;

namespace CustomNoMediatr.Posts;

public record CreatePost(string Title, string Body)
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

public sealed class CreatePostHandler 
{
    private readonly Database _database;

    public CreatePostHandler(Database database)
    {
        _database = database;
    }
    
    public async Task<bool> Handle(CreatePost request, CancellationToken cancellationToken)
    {
        var post = new Post()
        {
            Title = request.Title,
            Body = request.Body,
            CreateBy = request.UserId
        };

        _database.Posts.Add(post);
        return await _database.SaveChangesAsync(cancellationToken) == 1;
    }
}