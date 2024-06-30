using Azure.Messaging.ServiceBus.Administration;
using Demos_On_ServiceBus.Contracts;

namespace Demos_On_ServiceBus
{
    internal class CreateQueue : IQueueCreation
    {
        private readonly ServiceBusAdministrationClient serviceBusAdministrationClient;

        public CreateQueue()
        {
            serviceBusAdministrationClient = new ServiceBusAdministrationClient(ConnectionStrings.NamespaceConnectionString);
        }

        /// <summary>
        /// Creates a new Queue.
        /// </summary>
        /// <returns>void.</returns>
        public async Task CreateQueueAsync()
        {
            await serviceBusAdministrationClient.CreateQueueAsync(QueueNames.PlaceHolderQueueName);
        }
    }
}
