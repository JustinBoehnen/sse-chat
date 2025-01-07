namespace Server.Services;

public interface IMessageService
{
    /// <summary>
    /// Registers a listener for a specific recipient by creating a new channel for messages.
    /// </summary>
    void RegisterListener(object listener);

    /// <summary>
    /// Unregisters a listener for the specified recipient, removing their channel.
    /// </summary>
    void UnregisterListener(object listener);

    /// <summary>
    /// Sends a message to the recipient by writing to their channel.
    /// </summary>
    void NotifyNewMessageAvailable(Message message);

    /// <summary>
    /// Waits asynchronously for a new message for a specific recipient.
    /// </summary>
    Task<Message> WaitForNewMessageAsync(object listener, CancellationToken cancellationToken = default);
}