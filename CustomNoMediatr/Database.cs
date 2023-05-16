using CustomNoMediatr.Posts;
using Microsoft.EntityFrameworkCore;

namespace CustomNoMediatr;

public sealed class Database : DbContext
{
    public DbSet<Post> Posts { get; set; }

    public Database(DbContextOptions<Database> opt) : base(opt)
    { }

    public static void  InitializeDb(IServiceProvider sp)
    {
        using var scope = sp.CreateAsyncScope();
        var db = scope.ServiceProvider.GetService<Database>();

        db.Database.EnsureDeleted();
        db.Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Post>()
            .HasData(new Post[]
            {
                new Post
                {
                    Id = 1,
                    Title = "PostOne",
                    Body = "some random body",
                    CreateBy = "foo"
                },
                new Post
                {
                    Id = 2,
                    Title = "PostTwo",
                    Body = "some random body",
                    CreateBy = "bar"
                }
            });
    }

}