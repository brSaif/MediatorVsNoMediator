using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Benchos;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

BenchmarkRunner.Run<Benchmarks>();

[MemoryDiagnoser]
public class Benchmarks
{
    private IServiceProvider ServiceProvider { get; set; }

    public Benchmarks()
    {
        var sc = new ServiceCollection();

        sc.AddTransient<UserCommandPipeline>();
        sc.AddTransient<CreatePostHandler>();

        sc.AddMediatR(o =>
        {
            o.RegisterServicesFromAssemblyContaining<Benchmarks>();
            o.AddBehavior(typeof(IPipelineBehavior<,>), typeof(AddUserIdBehaviour<,>));
            o.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        });

        sc.AddValidatorsFromAssemblyContaining<Benchmarks>();

        ServiceProvider = sc.BuildServiceProvider();
    }

    [Benchmark]
    public Task Mediatr()
    {
        var mediatr = ServiceProvider.GetRequiredService<IMediator>();
        return mediatr.Send(new CreatePost("Title", "body"));
    }

    [Benchmark]
    public Task Custom()
    {
        var pipe = ServiceProvider.GetRequiredService<UserCommandPipeline>();
        var handler = ServiceProvider.GetRequiredService<CreatePostHandler>();

        return pipe.Pipe(
            new CreatePost("Title", "body"),
            handler.Handle,
            CancellationToken.None);
    }
}
