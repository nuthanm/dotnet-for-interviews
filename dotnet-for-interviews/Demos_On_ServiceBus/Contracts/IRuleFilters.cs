namespace Demos_On_ServiceBus.Contracts
{
    public interface ICorrelationRuleFilters
    {
        Task CreateSubscriptionEqualFilterAsync();

        Task SendMessageToTopicAsync(Employee emp);

        Task<string> ReceiveMessageFromTopicAsync();
    }

    public interface IBooleanRuleFilters
    {
        Task CreateSubscriptionTrueFilterAsync();

        Task CreateSubscriptionFalseFilterAsync();

        Task SendMessageToTopicAsync();

        Task<string> ReceiveMessageFromTopicAsync();
    }

    public interface ISqlRuleFilters
    {
        Task CreateSubscriptionSqlFilterAsync();

        Task SendMessageToTopicWithSqlFilterAsync(Employee emp);

        Task<string> ReceiveMessageFromTopicAsync();
    }
}
