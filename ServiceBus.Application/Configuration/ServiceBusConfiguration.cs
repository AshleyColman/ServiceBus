namespace ServiceBus.Application.Options
{
    public sealed class ServiceBusConfiguration
    {
        public string QueueName { get; init; } = null!;
        public int RecieveTimeout { get; init; }
    }
}
