using CustomNoMediatr;
using CustomNoMediatr.Posts;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddValidatorsFromAssemblyContaining<Program>();

builder.Services.AddDbContext<Database>( o =>
{
    o.UseSqlite($"Data Source=App.db");
});

var app = builder.Build();

Database.InitializeDb(app.Services);


app.Use(async (ctx, next) =>
{
    try
    {
        await next();
    }
    catch (ValidationException e)
    {
        await ctx.Response.WriteAsJsonAsync(e.Errors);
    }
});

app.MapGet("/posts", 
    (Database db, 
            CancellationToken ct) 
        => new ListPostsHandler(db).Handle(new ListPosts(), ct)
    );

app.MapPost("/post", 
    (Database db, 
            CreatePost request, 
            CancellationToken ct) 
        => new CreatePostHandler(db).Handle(request, ct)
    );

await app.RunAsync();