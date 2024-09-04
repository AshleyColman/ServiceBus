namespace ServiceBus.Application.Requests
{
    public class BatchAddMessagesRequest
    {
        public IReadOnlyCollection<Message>? Messages { get; init; }
    }
}
