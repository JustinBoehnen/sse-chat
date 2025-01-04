namespace SseChat.Data.Models;

public class Message
{
    public Guid Id { get; set; }
    public string User { get; set; } = null!;
    public string Text { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }
}
