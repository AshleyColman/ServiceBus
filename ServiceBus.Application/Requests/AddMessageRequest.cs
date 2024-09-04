namespace ServiceBus.Application.Requests
{
    public sealed class AddMessageRequest
    {
        public Message Message { get; set; } = null!;
    }
}
