namespace Demos_On_ServiceBus.Contracts
{
    public interface IQueueCreation
    {
        Task CreateQueueAsync();
    }

    public interface ITopicCreation
    {
        Task CreateTopicAsync();
    }

    public interface ISubscriptionCreation
    {
        Task CreateSubscriptionAsync();
    }
}
