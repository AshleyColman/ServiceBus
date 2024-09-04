using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Options;
using ServiceBus.Application.Options;
using ServiceBus.Application.Requests;
using ServiceBus.Interfaces.Services;
using System.Text.Json;

namespace ServiceBus.Application.Services
{
    public class ServiceBusService : IServiceBusService
    {
        private readonly ServiceBusConfiguration configuration;
        private readonly ServiceBusClient serviceBusClient;
        private readonly ServiceBusSender chatMessageQueueSender;

        public ServiceBusService(
            IOptions<ServiceBusConfiguration> configuration,
            ServiceBusClient serviceBusClient)
        {
            this.configuration = configuration.Value;
            this.serviceBusClient = serviceBusClient;
            this.chatMessageQueueSender = serviceBusClient.CreateSender(this.configuration.ChatMessageQueueName);
        }

        public async Task PostChatMessageToQueueAsync(PostMessageToQueueRequest request)
        {
            var message = new ServiceBusMessage(JsonSerializer.Serialize(request));

            try
            {
                await chatMessageQueueSender.SendMessageAsync(message);
            }
            catch (Exception)
            {

                throw;
            }


            await DisposeServiceBusAsync();
        }

        private async Task DisposeServiceBusAsync()
        {
            await chatMessageQueueSender.DisposeAsync();
            await serviceBusClient.DisposeAsync();
        }
    }
}
