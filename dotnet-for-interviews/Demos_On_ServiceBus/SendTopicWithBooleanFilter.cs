using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;
using Demos_On_ServiceBus.Contracts;

namespace Demos_On_ServiceBus
{
    public class SendTopicWithBooleanFilter : IBooleanRuleFilters
    {
        private readonly ServiceBusAdministrationClient administrationClient;
        private readonly ServiceBusClient serviceBusClient;
        public SendTopicWithBooleanFilter()
        {
            administrationClient = new ServiceBusAdministrationClient(ConnectionStrings.TopicConnectionString);
            serviceBusClient = new ServiceBusClient(ConnectionStrings.NamespaceConnectionString);
        }

        /// <summary>
        /// 1. Method Creates a new subscription under the specified queue.
        /// 2. Create a new Boolean filter with name: falseRule and it's value in under SQL Filter is 1=0
        /// </summary>
        /// <returns>void.</returns>
        public async Task CreateSubscriptionFalseFilterAsync()
        {
            var subscriberOptions = new CreateSubscriptionOptions(topicName: TopicNames.PlaceHolderTopicName, subscriptionName: SubscribersNames.PlaceHolderSubscriberName);

            var trueRuleFilter = new CreateRuleOptions("falseRule", new FalseRuleFilter());

            await administrationClient.CreateSubscriptionAsync(subscriberOptions, trueRuleFilter);
        }

        /// <summary>
        /// 1. Method Creates a new subscription under the specified queue.
        /// 2. Create a new Boolean filter with name: trueRule and it's value in under SQL Filter is 1=1
        /// </summary>
        /// <returns>void.</returns>
        public async Task CreateSubscriptionTrueFilterAsync()
        {
            var subscriberOptions = new CreateSubscriptionOptions(topicName: TopicNames.PlaceHolderTopicName, subscriptionName: SubscribersNames.PlaceHolderSubscriberName);

            var trueRuleFilter = new CreateRuleOptions("trueRule", new TrueRuleFilter());

            await administrationClient.CreateSubscriptionAsync(subscriberOptions, trueRuleFilter);
        }

        /// <summary>
        /// 1. Consider we have True and False filters
        /// 2. Now Sends a message to topic.
        /// As per this class example, Message should receive only if subscriber having 1=1 or TrueFilter
        /// </summary>
        /// <returns>void.</returns>
        public async Task SendMessageToTopicAsync()
        {
            var sbSender = serviceBusClient.CreateSender(TopicNames.PlaceHolderTopicName);

            await sbSender.SendMessageAsync(new ServiceBusMessage("Sample message to subscriber where it has true filter"));
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
