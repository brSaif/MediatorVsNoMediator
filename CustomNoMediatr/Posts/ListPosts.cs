using Microsoft.EntityFrameworkCore;

namespace CustomNoMediatr.Posts;

public record ListPosts();

public sealed class ListPostsHandler 
{
    private readonly Database _database;

    public ListPostsHandler(Database database)
    {
        _database = database;
    }

    public async Task<IEnumerable<Post>> Handle(ListPosts request, CancellationToken cancellationToken)
        => await _database.Posts.ToListAsync(cancellationToken);
}