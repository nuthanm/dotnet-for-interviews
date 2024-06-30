// See https://aka.ms/new-console-template for more information
using Azure.Messaging.ServiceBus;
using Demos_On_ServiceBus;
using System.Transactions;

#region Create_Queue_Or_Topic_Subscriber
var createQueue = new CreateQueue();
await createQueue.CreateQueueAsync();

var createTopic = new CreateTopic();
await createTopic.CreateTopicAsync();

var createSubscription = new CreateSubscription();
await createSubscription.CreateSubscriptionAsync();

#endregion

#region SendMessage(s)_Queue
var queueSend = new SendQueueMessages();
// Send plain text
await queueSend.SendMessageAsync("Plain text");

// Send Single Josn
await queueSend.SendMessageAsync(@"{""Name"":""Nani""}");

// Send List object
queueSend.SendMessages(new List<Employee>() { new() { Id = 1, Name = "Sree" } });

// Send Duplicate
await queueSend.SendUniqueMessageAsync("Plain text");

// Send Batch messages
await queueSend.SendBatchMessagesAsync();

#endregion

#region ReceiveMessage(s)_Queue
var queueReceive = new ReceiveQueueMesssage();

// Receive an object of data from Message
var employeeData = await queueReceive.ReceiveMessageAsync<Employee>();
if (employeeData != null)
{
    Console.WriteLine($"Employee Id: {employeeData.Id} and Name : {employeeData.Name}");
}

// Call Completed
await queueReceive.SetMessageToCompleteAsync();

// Call Deferred
await queueReceive.SetMessageToDeferredAsync();

// Call Abandon
await queueReceive.SetMessageToAbandonAsync();

// Call DLQ
await queueReceive.SetMessageToDLQAsync();

// Call Deferred and pass sequence
int sequenceNumber = 0;// Here you need to pass exact sequence number from queue
var empObject = await queueReceive.ReceiveMessageFromDeferredAsync<Employee>(sequenceNumber);
if (employeeData != null)
{
    Console.WriteLine($"From Deferred - Employee Id: {employeeData.Id} and Name : {employeeData.Name}");
}

// Read message from DLQ
empObject = await queueReceive.ReceiveMessageFromDLQAsync<Employee>();
if (employeeData != null)
{
    Console.WriteLine($"From DLQ - Employee Id: {employeeData.Id} and Name : {employeeData.Name}");
}

#endregion

#region ServiceBus_Cross_Entity_Transactions
// Create a client with transactions option as true
ServiceBusClient sb = new ServiceBusClient(ConnectionStrings.NamespaceConnectionString);

var sbClient = new ServiceBusClient("<Add_your_namespace_level_connectionString", new ServiceBusClientOptions { EnableCrossEntityTransactions = true });

// Receive a message from one Queue: Queue_1
var sbReceiverCET = sbClient.CreateReceiver("Queue_1");

// Write that message in Queue_2
var sbSenderCET1 = sbClient.CreateSender("Queue_2");

// Write that message in Queue_3
var sbSenderCET2 = sbClient.CreateSender("Queue_3");

// Read a message from Queue_1
var messageReceivedFromQueue_1 = await sbReceiverCET.ReceiveMessageAsync();

// Then only mark the queue_1 as completed and then delete in Queue_1
using (var tnx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
{
    await sbSenderCET1.SendMessageAsync(new ServiceBusMessage(messageReceivedFromQueue_1.Body.ToString()));

    await sbSenderCET2.SendMessageAsync(new ServiceBusMessage(messageReceivedFromQueue_1.Body.ToString()));

    await sbReceiverCET.CompleteMessageAsync(messageReceivedFromQueue_1);

    tnx.Complete();
}

#endregion

#region CreateOrSendOrReceive_CorrelationFilter_Topic

var correlationFilter = new SendTopicWithCorrelationFilter();

// Create
await correlationFilter.CreateSubscriptionEqualFilterAsync();

// Send
var emp = new Employee()
{
    Id = 1,
    Name = "Nani"
};

await correlationFilter.SendMessageToTopicAsync(emp);

// Receive
var receiverCorrelationMessageBody = await correlationFilter.ReceiveMessageFromTopicAsync();
Console.WriteLine($"receiverCorrelationMessageBody: {receiverCorrelationMessageBody}");

#endregion

#region CreateOrSendOrReceiveSQLRuleFilter

var sqlFilter = new SendTopicWithSQLFilter();

// Create
await sqlFilter.CreateSubscriptionSqlFilterAsync();

// Send
emp = new Employee()
{
    Id = 1,
    Name = "Nani"
};

await sqlFilter.SendMessageToTopicWithSqlFilterAsync(emp);

// Receive
var receiverSQLMessageBody = await sqlFilter.ReceiveMessageFromTopicAsync();
Console.WriteLine($"receiverSQLMessageBody: {receiverSQLMessageBody}");
#endregion

#region CreateOrSendOrReceive_BooleanFilter_Topic
var booleanFilter = new SendTopicWithBooleanFilter();

// Create
await booleanFilter.CreateSubscriptionTrueFilterAsync();
await booleanFilter.CreateSubscriptionFalseFilterAsync();

// Send
await booleanFilter.SendMessageToTopicAsync();

// Receive
var receiverBooleanObject = await booleanFilter.ReceiveMessageFromTopicAsync();
Console.WriteLine($"receiverBooleanObject: {receiverBooleanObject}");
#endregion

public class Employee
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
}
