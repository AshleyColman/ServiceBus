using ServiceBus.Domain.Domain.Enums;

namespace ServiceBus.Application.Requests
{
    public class Message
    {
        public required string From { get; init; } = null!;
        public required string To { get; init; } = null!;
        public required string Body { get; init; } = null!;
        public required ApplicationType Application { get; init; }
        public required QueueMessageType MessageType { get; init; }
    }
}
