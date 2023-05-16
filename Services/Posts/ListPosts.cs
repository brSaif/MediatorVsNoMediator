using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Services.Posts;

public record ListPosts : IRequest<IEnumerable<Post>>;

public sealed class ListPostsHandler : IRequestHandler<ListPosts, IEnumerable<Post>>
{
    private readonly Database _database;

    public ListPostsHandler(Database database)
    {
        _database = database;
    }

    public async Task<IEnumerable<Post>> Handle(ListPosts request, CancellationToken cancellationToken)
        => await _database.Posts.ToListAsync(cancellationToken);
}