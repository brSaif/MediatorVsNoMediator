using CustomNoMediatr.Posts;

namespace CustomNoMediatr;

public class Notifier
{
    private readonly IServiceProvider _sp;

    public Notifier(IServiceProvider sp)
    {
        _sp = sp;
    }

    public Task Handle<T>(T notification, CancellationToken cancellationToken = default)
        =>
            Task.WhenAll(_sp.GetRequiredService<IEnumerable<INotificationHandler<T>>>()
                .Select(n => n.Handle(notification, cancellationToken))
            );
}