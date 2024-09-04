namespace ServiceBus.Interfaces.Services
{
    public interface IServiceBusService
    {
        Task PostChatMessageToQueueAsync(PostMessageToQueueRequest request);
    }
}
