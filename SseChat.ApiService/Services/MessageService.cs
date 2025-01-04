using SseChat.Data.Models;

namespace SseChat.ApiService.Services;

public class MessageService : IMessageService
{
    private TaskCompletionSource<Message?> _tcs = new();
    private long _id = 0;

    public void Reset()
    {
        _tcs = new();
    }

    public void NotifyNewMessageAvailable()
    {
        _tcs.TrySetResult(new Message()
        {
            Id = Guid.NewGuid(),
            User = "System",
            Text = "New message available",
            CreatedAt = DateTimeOffset.Now
        });
    }

    public Task<Message?> WaitForNewMessageAsync(CancellationToken cancellationToken = default)
    {
        Task.Run(async () =>
        {
            await Task.Delay(TimeSpan.FromSeconds(Random.Shared.Next(0, 5)));
            NotifyNewMessageAvailable();
        });

        return _tcs.Task;
    }
}
