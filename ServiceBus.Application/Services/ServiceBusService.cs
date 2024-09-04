using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ServiceBus.Application.Options;
using ServiceBus.Application.Requests;
using ServiceBus.Interfaces.Services;
using System.Net.Mime;
using System.Text.Json;

namespace ServiceBus.Application.Services
{
    public class ServiceBusService : IServiceBusService
    {
        private readonly ILogger<ServiceBusService> _logger;
        private readonly ServiceBusConfiguration _configuration;
        private readonly ServiceBusClient _serviceBusClient;
        private readonly ServiceBusSender _queueSender;
        private readonly ServiceBusReceiver _queueReciever;

        public ServiceBusService(
            ILogger<ServiceBusService> logger,
            IOptions<ServiceBusConfiguration> configuration,
            ServiceBusClient serviceBusClient)
        {
            _logger = logger;
            _configuration = configuration.Value;
            _serviceBusClient = serviceBusClient;
            _queueSender = serviceBusClient.CreateSender(_configuration.QueueName);
            _queueReciever = serviceBusClient.CreateReceiver(_configuration.QueueName);    
        }

        public async Task<ServiceBusReceivedMessage?> PeekMessageAsync()
        {
            ServiceBusReceivedMessage message = await _queueReciever.PeekMessageAsync();

            if (message is null)
                _logger.LogInformation("Unable to peek message, no messages in the queue");

            return message;
        }

        public async Task<ServiceBusReceivedMessage?> CompleteMessageAsync()
        {
            ServiceBusReceivedMessage message = await _queueReciever.ReceiveMessageAsync(TimeSpan.FromMinutes(_configuration.RecieveTimeout));

            await _queueReciever.CompleteMessageAsync(message);

            return message;
        }

        public async Task AddMessageAsync(AddMessageRequest request)
        {
            var message = new ServiceBusMessage(JsonSerializer.Serialize(request));

            message.ContentType = MediaTypeNames.Application.Json;

            await _queueSender.SendMessageAsync(message);
        }

        public async Task<ICollection<ServiceBusMessage>> BatchAddMessagesAsync(IReadOnlyCollection<Message> messages)
        {
            ServiceBusMessageBatch messageBatch = await _queueSender.CreateMessageBatchAsync();

            ICollection<ServiceBusMessage> result = [];

            foreach (var message in messages) 
            {
                ServiceBusMessage? batchMessage = new(JsonSerializer.Serialize(message));

                messageBatch.TryAddMessage(batchMessage);
                result.Add(batchMessage);
            }

            await _queueSender.SendMessagesAsync(messageBatch);

            return result;
        }
    }
}
