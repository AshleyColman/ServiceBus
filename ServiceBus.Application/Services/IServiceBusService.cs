using ServiceBus.Application.Requests;

namespace ServiceBus.Interfaces.Services
{
    public interface IServiceBusService
    {
        Task PostChatMessageToQueueAsync(PostMessageToQueueRequest request);
    }
}
