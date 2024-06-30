using Azure.Messaging.ServiceBus.Administration;
using Demos_On_ServiceBus.Contracts;

namespace Demos_On_ServiceBus
{
    internal class CreateTopic : ITopicCreation
    {
        private readonly ServiceBusAdministrationClient serviceBusAdministrationClient;

        public CreateTopic()
        {
            serviceBusAdministrationClient = new ServiceBusAdministrationClient(ConnectionStrings.NamespaceConnectionString);
        }

        /// <summary>
        /// Creates a new topic.
        /// </summary>
        /// <returns>void.</returns>
        public async Task CreateTopicAsync()
        {
            await serviceBusAdministrationClient.CreateQueueAsync(QueueNames.PlaceHolderQueueName);
        }
    }
}
