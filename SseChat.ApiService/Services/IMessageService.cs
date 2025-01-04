using SseChat.Data.Models;

namespace SseChat.ApiService.Services
{
    public interface IMessageService
    {
        void NotifyNewMessageAvailable();
        void Reset();
        Task<Message?> WaitForNewMessageAsync(CancellationToken cancellationToken = default);
    }
}