using Azure.Messaging.ServiceBus.Administration;
using Demos_On_ServiceBus.Contracts;

namespace Demos_On_ServiceBus
{
    internal class CreateSubscription : ISubscriptionCreation
    {
        private readonly ServiceBusAdministrationClient serviceBusAdministrationClient;

        public CreateSubscription()
        {
            serviceBusAdministrationClient = new ServiceBusAdministrationClient(ConnectionStrings.NamespaceConnectionString);
        }

        /// <summary>
        /// Creates a new topic.
        /// </summary>
        /// <returns>void.</returns>
        public async Task CreateSubscriptionAsync()
        {
            await serviceBusAdministrationClient.CreateSubscriptionAsync(TopicNames.PlaceHolderTopicName, SubscribersNames.PlaceHolderSubscriberName);
        }
    }
}
