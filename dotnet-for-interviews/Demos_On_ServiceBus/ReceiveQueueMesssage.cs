using Azure.Messaging.ServiceBus;
using Demos_On_ServiceBus.Contracts;
using System.Text.Json;

namespace Demos_On_ServiceBus
{
    internal class ReceiveQueueMesssage : IReceiveMessages
    {
        private readonly ServiceBusClient serviceBusClient;
        private ServiceBusReceiver sbReceiver;
        private ServiceBusReceivedMessage messageReceiver;

        public ReceiveQueueMesssage()
        {
            serviceBusClient = new ServiceBusClient(ConnectionStrings.NamespaceConnectionString);
        }

        /// <summary>
        /// Receive a single message and deserialize into an object.
        /// </summary>
        /// <returns>Object Data</returns>
        public async Task<T?> ReceiveMessageAsync<T>()
        {
            // Step 1 & 2: Create a receiver object and read the message from receiver
            (sbReceiver, messageReceiver) = await GetReceiverObject(sbReceiver, messageReceiver);

            // Step 3: Deserialize into an object
            return GetDeserializeDataAsync<T>(messageReceiver);
        }

        /// <summary>
        /// After receiving the message we set this message to abondan.
        /// </summary>
        /// <returns>void.</returns>
        public async Task SetMessageToAbandonAsync()
        {
            // Step 1 & 2: Create a receiver object and read the message from receiver
            (sbReceiver, messageReceiver) = await GetReceiverObject(sbReceiver, messageReceiver);

            // Step 3: To abondon the receive message
            // This time DeliveryCount increased by 1
            await sbReceiver.AbandonMessageAsync(messageReceiver);
        }

        /// <summary>
        /// After receiving the message we set this message to completed so that it removes from queue.
        /// </summary>
        /// <returns>void</returns>
        public async Task SetMessageToCompleteAsync()
        {

            // Step 1 & 2: Create a receiver object and read the message from receiver
            (sbReceiver, messageReceiver) = await GetReceiverObject(sbReceiver, messageReceiver);


            // Step 3: To set message processed/Completed explicity
            await sbReceiver.CompleteMessageAsync(messageReceiver);
        }

        /// <summary>
        /// Increase Delivery Count by 1 and change state from Active to Deferred and message still be in that queue.
        /// </summary>
        /// <returns>void.</returns>
        public async Task SetMessageToDeferredAsync()
        {

            // Step 1 & 2: Create a receiver object and read the message from receiver
            (sbReceiver, messageReceiver) = await GetReceiverObject(sbReceiver, messageReceiver);

            // Step 3: To set Deferred
            // Increase Delivery Count by 1 and change state from Active to Deferred
            // Still message exists in Queue

            await sbReceiver.DeferMessageAsync(messageReceiver);
        }

        /// <summary>
        /// Move the message into Deadletter queue or DLQ and Move from Active Queue to DLQ
        /// </summary>
        /// <returns></returns>
        public async Task SetMessageToDLQAsync()
        {
            // Step 1 & 2: Create a receiver object and read the message from receiver
            (sbReceiver, messageReceiver) = await GetReceiverObject(sbReceiver, messageReceiver);

            // Step 3: Move the message into Deadletter queue or DLQ
            // Move from Active Queue to DLQ
            await sbReceiver.DeadLetterMessageAsync(messageReceiver);
        }

        /// <summary>
        /// Receive the message from Deferred Queue type based out of sequence number.
        /// </summary>
        /// <typeparam name="T">Obj</typeparam>
        /// <param name="sequenceNumber">sequenceNumber</param>
        /// <returns>object.</returns>
        public async Task<T?> ReceiveMessageFromDeferredAsync<T>(int sequenceNumber)
        {
            // Step 1 & 2: Create a receiver object and read the message from receiver
            (sbReceiver, messageReceiver) = await GetReceiverObject(sbReceiver, messageReceiver);

            // Step 3:
            // To access this deferred messages we need Sequence Number == 16 for an example.
            // 90 line to comment when you are calling the below line
            var messageObject = await sbReceiver.ReceiveDeferredMessageAsync(16);

            // Step 4: Deserialize into an object
            return GetDeserializeDataAsync<T>(messageObject);
        }

        /// <summary>
        /// Receive message from DLQ.
        /// </summary>
        /// <typeparam name="T">type of object.</typeparam>
        /// <returns>object.</returns>
        public async Task<T?> ReceiveMessageFromDLQAsync<T>()
        {
            // Step 1: Create receiver object
            sbReceiver = serviceBusClient.CreateReceiver(QueueNames.PlaceHolderQueueName, new ServiceBusReceiverOptions() { SubQueue = SubQueue.DeadLetter });

            // Step 2: Receive a message
            messageReceiver = await sbReceiver.ReceiveMessageAsync();

            // Step 3: Deserialize into an object
            return GetDeserializeDataAsync<T>(messageReceiver);
        }

        /// <summary>
        /// Create a ServiceBusReceiver with PeekLock and This is a default one and message won't delete after complete
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<T?> ReceiveMessageFromDLQPeekLockModeAsync<T>()
        {
            // Step 1: Create receiver object
            sbReceiver = serviceBusClient.CreateReceiver(QueueNames.PlaceHolderQueueName, new ServiceBusReceiverOptions() { ReceiveMode = ServiceBusReceiveMode.PeekLock });

            // Step 2: Receive a message
            messageReceiver = await sbReceiver.ReceiveMessageAsync();

            // Step 3: Deserialize into an object
            return GetDeserializeDataAsync<T>(messageReceiver);
        }

        /// <summary>
        /// Create a ServiceBusReceiver with ReceiveAndDelete and This will delete the message after processed.
        /// </summary>
        /// <typeparam name="T">T.</typeparam>
        /// <returns>Object.</returns>
        public async Task<T?> ReceiveMessageFromDLQReceiveAndDeleteModeAsync<T>()
        {
            // Step 1: Create receiver object
            sbReceiver = serviceBusClient.CreateReceiver(QueueNames.PlaceHolderQueueName, new ServiceBusReceiverOptions() { ReceiveMode = ServiceBusReceiveMode.ReceiveAndDelete });

            // Step 2: Receive a message
            messageReceiver = await sbReceiver.ReceiveMessageAsync();

            // Step 3: Deserialize into an object
            return GetDeserializeDataAsync<T>(messageReceiver);
        }

        private async Task<(ServiceBusReceiver, ServiceBusReceivedMessage)> GetReceiverObject(ServiceBusReceiver sbReceiver, ServiceBusReceivedMessage messageReceiver)
        {
            // Step 1: Create receiver object
            sbReceiver = serviceBusClient.CreateReceiver(QueueNames.PlaceHolderQueueName);

            // Step 2: Receive a message
            messageReceiver = await sbReceiver.ReceiveMessageAsync();

            return (sbReceiver, messageReceiver);
        }


        private static T? GetDeserializeDataAsync<T>(ServiceBusReceivedMessage messageObject)
        {
            var objDeserializeData = JsonSerializer.Deserialize<T>(messageObject.Body.ToString());

            if (objDeserializeData != null)
            {
                return objDeserializeData;
            }
            return default;
        }
    }
}
