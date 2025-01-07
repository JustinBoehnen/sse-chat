using System.Collections.Concurrent;
using System.Threading.Channels;

namespace Server.Services;

public class MessageService : IMessageService
{
    private readonly ConcurrentDictionary<object, Channel<Message>> _channels = new();

    public void RegisterListener(object listener)
    {
        if (!_channels.TryAdd(listener, Channel.CreateUnbounded<Message>()))
        {
            throw new InvalidOperationException("Listener already registered.");
        }
    }

    public void UnregisterListener(object listener)
    {
        if (!_channels.TryRemove(listener, out _))
        {
            throw new InvalidOperationException("Listener not registered.");
        }
    }

    public void NotifyNewMessageAvailable(Message message)
    {
        foreach (var channel in _channels.Values)
        {
            _ = Task.Run(async () => await channel.Writer.WriteAsync(message));
        }
    }

    public async Task<Message> WaitForNewMessageAsync(object listener, CancellationToken cancellationToken = default)
    {
        if (!_channels.TryGetValue(listener, out var channel))
        {
            throw new InvalidOperationException($"Listener not registered.");
        }

        return await channel.Reader.ReadAsync(cancellationToken);
    }
}