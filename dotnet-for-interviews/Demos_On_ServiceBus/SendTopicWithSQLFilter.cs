using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;
using Demos_On_ServiceBus.Contracts;
using System.Text.Json;

namespace Demos_On_ServiceBus
{
    internal class SendTopicWithSQLFilter : ISqlRuleFilters
    {
        private readonly ServiceBusAdministrationClient administrationClient;
        private readonly ServiceBusClient serviceBusClient;
        public SendTopicWithSQLFilter()
        {
            administrationClient = new ServiceBusAdministrationClient(ConnectionStrings.TopicConnectionString);
            serviceBusClient = new ServiceBusClient(ConnectionStrings.NamespaceConnectionString);
        }

        /// <summary>
        /// 1. Method Creates a new subscription under the specified queue.
        /// 2. Create a new SQLRule filter with name: sqlNameRule and it's value in under SQL Filter is Key = Name, Value = Nani
        /// </summary>
        /// <returns>void.</returns>
        public async Task CreateSubscriptionSqlFilterAsync()
        {
            var subscriberOptions = new CreateSubscriptionOptions(topicName: TopicNames.PlaceHolderTopicName, subscriptionName: SubscribersNames.PlaceHolderSubscriberName);

            var sqlRuleFilter = new CreateRuleOptions("sqlNameRule", new SqlRuleFilter("Name=='Nani'"));

            await administrationClient.CreateSubscriptionAsync(subscriberOptions, sqlRuleFilter);
        }

        /// <summary>
        /// 1. SQLRule is available
        /// 2. Now Sends a message to topic with Custom properties.
        /// 3. Message should add to the subscriber with filter matches: Name == Nani        
        /// </summary>
        /// <returns>void.</returns>
        public async Task SendMessageToTopicWithSqlFilterAsync(Employee emp)
        {
            // Step 1: Serialize Object => It means convert into Json string
            var empInJson = JsonSerializer.Serialize(emp);

            // Step 2: Add that json directly to servicebusMessage if not Encoding.utf8.Byte method
            var sbMessageObjectData = new ServiceBusMessage(empInJson)
            {
                ApplicationProperties =
                {
                    {
                        "Name", "Nani"
                    }
                }
            };

            // Step 3: Add this message to Sender method.
            var sbSenderObject = serviceBusClient.CreateSender(TopicNames.PlaceHolderTopicName);

            // Step 4: Your message is in json format with filters.
            await sbSenderObject.SendMessageAsync(sbMessageObjectData);
        }


        /// <summary>
        /// Receives a message and send back the received message
        /// </summary>
        /// <returns>message.</returns>
        public async Task<string> ReceiveMessageFromTopicAsync()
        {
            var receiverObject = serviceBusClient.CreateReceiver(TopicNames.PlaceHolderTopicName, subscriptionName: SubscribersNames.PlaceHolderSubscriberName);

            var msgReceiver = await receiverObject.ReceiveMessageAsync();

            return msgReceiver.Body.ToString();
        }
    }
}
