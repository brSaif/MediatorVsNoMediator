using CustomNoMediatr;
using CustomNoMediatr.Posts;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Services.Posts;

var builder = WebApplication.CreateBuilder(args);

var types = typeof(Program)
    .Assembly
    .GetTypes()
    .Where(x => (x.Name.EndsWith("Handler") || x.Name.EndsWith("Pipeline")) 
                && !x.IsAbstract && !x.IsInterface);

foreach (var type in types)
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
        var name = ctx.GetEndpoint();
        if (name != null)
        {

            var _logger = ctx.RequestServices.GetRequiredService<ILogger<Program>>();

            _logger.LogInformation("Starting execution: '{name}'",name);
            await next();
            _logger.LogInformation("Finished executing '{name}'", name);
        }
        else
        {
            await next();
        }
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
            UserCommandPipeline pipe,
            CancellationToken ct) 
        => pipe.Pipe(request, handler.Handle, ct)
    );

app.MapGet("/notification", 
    async (PingHandler pingHandler, 
        PongHandler pongHandler) 
        =>
{
    var id = Guid.NewGuid().ToString();
    await Task.WhenAll( 
        pingHandler.Handle(new Ping(){Message = id}, CancellationToken.None),
        pongHandler.Handle(new Pong(){Message = id}, CancellationToken.None)
        );
    
    return "ok";
});

await app.RunAsync();