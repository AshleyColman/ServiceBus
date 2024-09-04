using Azure.Messaging.ServiceBus;
using ServiceBus.Application.Requests;

namespace ServiceBus.Interfaces.Services
{
    public interface IServiceBusService
    {
        Task<ServiceBusReceivedMessage?> PeekMessageAsync();
        Task<ServiceBusReceivedMessage?> CompleteMessageAsync();
        Task AddMessageAsync(AddMessageRequest request);
        Task<ICollection<ServiceBusMessage>> BatchAddMessagesAsync(IReadOnlyCollection<Message> messages);
    }
}
