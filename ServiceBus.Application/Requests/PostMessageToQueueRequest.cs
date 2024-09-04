using ServiceBus.Domain.Domain.Enums;

namespace ServiceBus.Application.Requests
{
    public sealed class PostMessageToQueueRequest
    {
        public required string From { get; init; } = null!;
        public required string To { get; init; } = null!;
        public required string Message { get; init; } = null!;
        public required ApplicationType Application { get; init; }
        public required QueueMessageType MessageType { get; init; }
    }
}
