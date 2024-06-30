using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;
using Demos_On_ServiceBus.Contracts;
using System.Text.Json;

namespace Demos_On_ServiceBus
{
    internal class SendTopicWithCorrelationFilter : ICorrelationRuleFilters
    {

        private readonly ServiceBusAdministrationClient administrationClient;
        private readonly ServiceBusClient serviceBusClient;
        public SendTopicWithCorrelationFilter()
        {
            administrationClient = new ServiceBusAdministrationClient(ConnectionStrings.TopicConnectionString);
            serviceBusClient = new ServiceBusClient(ConnectionStrings.NamespaceConnectionString);
        }

        /// <summary>
        /// 1. Method Creates a new subscription under the specified queue.
        /// 2. Create a new Correlation filter with name: correlationEqaulRule and it's value in under Correlation Filter is Key = Name, Value = Nani
        /// </summary>
        /// <returns>void.</returns>
        public async Task CreateSubscriptionEqualFilterAsync()
        {
            var subscriberOptions = new CreateSubscriptionOptions(topicName: TopicNames.PlaceHolderTopicName, subscriptionName: SubscribersNames.PlaceHolderSubscriberName);

            var correlationEqualFilter = new CreateRuleOptions("correlationEqaulRule",
                new CorrelationRuleFilter()
                {
                    ApplicationProperties =
                    {
                        { "Key", "Value" } // Ex: key == "name", value == "nani"
                    }
                });

            await administrationClient.CreateSubscriptionAsync(subscriberOptions, correlationEqualFilter);
        }

        public async Task SendMessageToTopicAsync(Employee emp)
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
