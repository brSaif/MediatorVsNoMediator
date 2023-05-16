using CustomNoMediatr;
using CustomNoMediatr.Posts;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

foreach (var type in typeof(Program).Assembly.GetTypes().Where(x => x.Name.EndsWith("Handler") && !x.IsAbstract && !x.IsInterface))
{
    builder.Services.AddTransient(type);
}

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
    (ListPostsHandler handler, 
            CancellationToken ct) 
        => handler.Handle(new ListPosts(), ct)
    );

app.MapPost("/post", 
    (CreatePostHandler handler, 
            CreatePost request, 
            CancellationToken ct) 
        => handler.Handle(request, ct)
    );

await app.RunAsync();