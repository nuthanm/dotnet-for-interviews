using Azure.Messaging.ServiceBus;
using Demos_On_ServiceBus.Contracts;
using System.Text.Json;

namespace Demos_On_ServiceBus
{
    internal class SendQueueMessages : ISendMessages
    {
        private readonly ServiceBusClient serviceBusClient;
        private ServiceBusSender sbSender;
        private ServiceBusMessage sbMessage;

        public SendQueueMessages()
        {
            serviceBusClient = new ServiceBusClient(ConnectionStrings.NamespaceConnectionString);
        }

        /// <summary>
        /// Send a plain string or json object as a message.
        /// </summary>
        /// <param name="message">message.</param>
        /// <returns>void.</returns>
        public async Task SendMessageAsync(string message)
        {
            // Step 1 : Create sender and message object.
            (sbSender, sbMessage) = GetSenderAsync(message, QueueNames.PlaceHolderQueueName);

            // Step 3: Send message
            await sbSender.SendMessageAsync(sbMessage);

        }

        /// <summary>
        /// 1. Generic method access any class and serialize
        /// 2. Send this message to queue.
        /// </summary>
        /// <typeparam name="T">Class.</typeparam>
        /// <param name="obj">obj of type class</param>
        /// <returns>void.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task SendMessageAsync<T>(T obj)
        {
            // Step 1: Serialize the object
            var messageInJson = JsonSerializer.Serialize<T>(obj);

            // Step 2 & 3 : Create sender and message object.
            (sbSender, sbMessage) = GetSenderAsync(messageInJson, QueueNames.PlaceHolderQueueName);

            // Step 4: Send message
            await sbSender.SendMessageAsync(sbMessage);
        }

        /// <summary>
        /// Sends multiple messages at once.
        /// </summary>
        /// <typeparam name="T">List of object</typeparam>
        /// <param name="obj">obj</param>
        /// <returns>void.</returns>
        public void SendMessages<T>(List<T> obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This method contains two scenarios
        /// Scenario 1: With out any message properties - created two entries with same contnet
        /// Scenario 2: With message properties: MessageId - created only one entry.
        /// </summary>
        /// <param name="message">message.</param>
        /// <returns>void.</returns>
        public async Task SendUniqueMessageAsync(string message)
        {

            // Step 1 & 2 : Create sender and message object.
            (sbSender, sbMessage) = GetSenderAsync(message, QueueNames.PlaceHolderQueueName);
            (sbSender, var sbMessage1) = GetSenderAsync(message, QueueNames.PlaceHolderQueueName);

            // Send a message(s) => Two times two entries added in queue
            await sbSender.SendMessageAsync(sbMessage);
            await sbSender.SendMessageAsync(sbMessage1);

            // Send a message(s) => Two times two entries added in queue
            var sbMessage3 = new ServiceBusMessage(message);
            sbMessage3.MessageId = "123";

            var sbMessage4 = new ServiceBusMessage(message);
            sbMessage3.MessageId = "123";

            // Send a message(s) => Only one entry because for both messages having same messageId
            await sbSender.SendMessageAsync(sbMessage3);
            await sbSender.SendMessageAsync(sbMessage4);
        }


        /// <summary>
        /// This method sends batch number of messages.
        /// </summary>
        /// <param name="message">message.</param>
        /// <returns>void.</returns>
        public async Task SendBatchMessagesAsync()
        {

            // Step 1: Create sender object.
            sbSender = serviceBusClient.CreateSender(QueueNames.PlaceHolderQueueName);

            // Step 2: Creating a batch of messages
            var sbCreateBathMessager = await sbSender.CreateMessageBatchAsync();
            sbCreateBathMessager.TryAddMessage(new ServiceBusMessage("Message 1"));
            sbCreateBathMessager.TryAddMessage(new ServiceBusMessage("Message 2"));

            // Send multiple messages at once.
            await sbSender.SendMessagesAsync(sbCreateBathMessager);
        }

        /// <summary>
        /// This method sends batch number of messages.
        /// </summary>
        /// <param name="numberOfMessages">numberOfMessages.</param>
        /// <returns>void.</returns>
        public async Task SendBatchMessagesAsync(int numberOfMessages)
        {
            // Step 1: Create sender object.
            sbSender = serviceBusClient.CreateSender(QueueNames.PlaceHolderQueueName);

            // Step 2: Creating a batch of messages
            var sbCreateBathMessager = await sbSender.CreateMessageBatchAsync();

            // Ex: If numberOfMessages = 1000 means 1000 messages we sent to queue
            // Reason for this use case is : Queue Processing order is not in FIFO by default
            for (int i = 1; i < numberOfMessages; i++)
            {
                sbCreateBathMessager.TryAddMessage(new ServiceBusMessage($"Message {i}"));
            }

            // Send multiple messages at once.
            await sbSender.SendMessagesAsync(sbCreateBathMessager);
        }



        /// <summary>
        /// This method sends batch number of messages with sessionId to receive in sequence order.
        /// While creating queue/Subscriptions we should enable this Enable Session option.
        /// </summary>
        /// <param name="numberOfMessages">numberOfMessages.</param>
        /// <returns>void.</returns>
        public async Task SendBatchMessagesWithSessionIdAsync(int numberOfMessages)
        {
            // Step 1: Create sender object.
            sbSender = serviceBusClient.CreateSender(QueueNames.PlaceHolderQueueName);

            // Step 2: Creating a batch of messages
            var sbCreateBathMessager = await sbSender.CreateMessageBatchAsync();

            // Ex: If numberOfMessages = 1000 means 1000 messages we sent to queue
            // Reason for this use case is : Queue Processing order is not in FIFO by default

            for (int i = 1; i < numberOfMessages; i++)
            {
                var serviceBusMessage = new ServiceBusMessage($"Message: {i}");
                serviceBusMessage.SessionId = "123"; // This will make queues in FIFO
                sbCreateBathMessager.TryAddMessage(serviceBusMessage);
            }

            // To make this in FIFO
            // Ex: In Azure function app => IsSessionEnabled = true converts into FIFO
            // Send multiple messages at once.
            await sbSender.SendMessagesAsync(sbCreateBathMessager);
        }

        private (ServiceBusSender, ServiceBusMessage) GetSenderAsync(string message, string QueueName)
        {
            // Step 1: Create sender object
            sbSender = serviceBusClient.CreateSender(QueueName);

            // Step 2: Create messagebody object
            sbMessage = new ServiceBusMessage(message);

            return (sbSender, sbMessage);
        }
    }
}
