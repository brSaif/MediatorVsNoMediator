using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Services;
using Services.Behaviours;
using Services.Posts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR( o =>
{
    o.RegisterServicesFromAssemblyContaining<Database>();
    o.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
    o.AddBehavior(typeof(IPipelineBehavior<,>), typeof(AddUserIdBehaviour<,>));
    o.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
});

builder.Services.AddValidatorsFromAssemblyContaining<CreatePost>();

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

app.MapGet("/posts", (IMediator mediatr) => mediatr.Send(new ListPosts()));
app.MapPost("/post", (CreatePost request, IMediator mediatr) => mediatr.Send(request));

await app.RunAsync();