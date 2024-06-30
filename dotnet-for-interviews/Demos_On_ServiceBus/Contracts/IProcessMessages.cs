namespace Demos_On_ServiceBus.Contracts
{
    public interface ISendMessages
    {
        Task SendMessageAsync(string message);

        Task SendMessageAsync<T>(T obj);

        void SendMessages<T>(List<T> obj);

        Task SendUniqueMessageAsync(string message);

        Task SendBatchMessagesAsync();
    }

    public interface IReceiveMessages
    {
        Task<T?> ReceiveMessageAsync<T>();

        Task SetMessageToCompleteAsync();

        Task SetMessageToAbandonAsync();

        Task SetMessageToDeferredAsync();

        Task SetMessageToDLQAsync();

        Task<T?> ReceiveMessageFromDeferredAsync<T>(int sequenceNumber);

        Task<T?> ReceiveMessageFromDLQAsync<T>();

        Task<T?> ReceiveMessageFromDLQPeekLockModeAsync<T>();

        Task<T?> ReceiveMessageFromDLQReceiveAndDeleteModeAsync<T>();
    }
}
