// See https://aka.ms/new-console-template for more information
using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;
using System.Text.Json;
using System.Transactions;


// Create a ServiceBusClient
ServiceBusClient sb = new ServiceBusClient("<Add your service bus connection string>");


#region ServiceBus_SendMessage
Console.WriteLine("Demos on Azure Servicebus - Begin Sending");

// Create a ServiceBusSender
ServiceBusSender sbSender = sb.CreateSender("<Add_Your_Queue_Or_TopicName");

// Prepare a message
ServiceBusMessage sbMessage = new ServiceBusMessage("<Add your message_or_json_string");

// Send a message
await sbSender.SendMessageAsync(sbMessage);

Console.WriteLine("Demos on Azure Servicebus - End");

#endregion

#region ServiceBus_ReceiveMessage
Console.WriteLine("Demos on Azure Servicebus - Begin Receving");

// Create a ServiceBusSender
ServiceBusReceiver sbReceiver = sb.CreateReceiver("<Add_Your_Queue_Or_TopicName");

// Send a message
var sbReceivedMessage = await sbReceiver.ReceiveMessageAsync();

Console.WriteLine($"Message what we received: {sbReceivedMessage.Body}");

Console.WriteLine("Demos on Azure Servicebus - End");

#endregion

#region ServiceBus_SendObjectMessage

// Step 1: Prepare an object
var emp = new Employee()
{
    Id = 1,
    Name = "Nani"
};

// Step 2: Serialize Object => It means convert into Json string
var empInJson = JsonSerializer.Serialize(emp);

// Step 3: Add that json directly to servicebusMessage if not Encoding.utf8.Byte method
ServiceBusMessage sbMessageObjectData = new ServiceBusMessage(empInJson);

// Step 4: Add this message to Sender method.
ServiceBusSender sbSenderObject = sb.CreateSender("<Queue_Name");

// Step 5: Your message is in json format.
await sbSenderObject.SendMessageAsync(sbMessageObjectData);

#endregion

#region ServiceBus_ReceiveObjectMessage

// Create a ServiceBusReceiver
ServiceBusReceiver sbReceiverObject = sb.CreateReceiver("<Add_Your_Queue_Or_TopicName");

// Receives a message
var sbReceivedMessageObject = await sbReceiver.ReceiveMessageAsync();
var msgObject = sbReceivedMessageObject.Body.ToString();

// Deserialize into an Object
var empInObj = JsonSerializer.Deserialize<Employee>(msgObject);

// Display the name in console.
Console.WriteLine($"Message what we received and emp Name is : {empInObj?.Name}");

// To set message processed/Completed explicity
await sbReceiverObject.CompleteMessageAsync(sbReceivedMessageObject);

// To abondon the receive message
// This time DeliveryCount increased by 1
await sbReceiverObject.AbandonMessageAsync(sbReceivedMessageObject);

// To set Deferred
// Increase Delivery Count by 1 and change state from Active to Deferred
// Still message exists in Queue

await sbReceiverObject.DeferMessageAsync(sbReceivedMessageObject);

// To access this deferred messages we need Sequence Number == 16 for an example.
// 90 line to comment when you are calling the below line
await sbReceiverObject.ReceiveDeferredMessageAsync(16);

// Move the message into Deadletter queue or DLQ
// Move from Active Queue to DLQ
await sbReceiverObject.DeadLetterMessageAsync(sbReceivedMessageObject);

// How to read a message from DLQ

#endregion

#region ReceiveMessageFromDLQ

// Create a ServiceBusReceiver from DLQ
var sbReceiverObjectFromDLQ = sb.CreateReceiver("<Add_Your_Queue_Or_TopicName", new ServiceBusReceiverOptions() { SubQueue = SubQueue.DeadLetter });

// Receives a message
var sbReceivedMessageObjectFromDLQ = await sbReceiverObjectFromDLQ.ReceiveMessageAsync();
var msgObjectFromDLQ = sbReceivedMessageObjectFromDLQ.Body.ToString();

// Deserialize into an Object
var empInObjFromDLQ = JsonSerializer.Deserialize<Employee>(msgObjectFromDLQ);

// Display the name in console.
Console.WriteLine($"Message what we received and emp Name is : {empInObj?.Name}");

#endregion

#region SampleConfiguration_On_PeekLock_Vs_ReceiveAndDeletee

// Create a ServiceBusReceiver with PeekLock
// This is a default one and message won't delete after complete
// TODO: I need to check
var sbReceiverObjectPeekLock = sb.CreateReceiver("<Add_Your_Queue_Or_TopicName", new ServiceBusReceiverOptions() { ReceiveMode = ServiceBusReceiveMode.PeekLock });

// Receives a message
var sbReceivedMessageObjectWithPeekLock = await sbReceiverObjectPeekLock.ReceiveMessageAsync();
var msgObjectWithPeekLock = sbReceivedMessageObjectFromDLQ.Body.ToString();

// Deserialize into an Object
var empInObjWithPeekLock = JsonSerializer.Deserialize<Employee>(msgObjectWithPeekLock);

// Display the name in console.
Console.WriteLine($"Message what we received and emp Name is : {empInObjWithPeekLock?.Name}");

// Create a ServiceBusReceiver with PeekLock
// This is a default one and message won't delete after complete
// TODO: I need to check
var sbReceiverObjectReceiveAndDelete = sb.CreateReceiver("<Add_Your_Queue_Or_TopicName", new ServiceBusReceiverOptions() { ReceiveMode = ServiceBusReceiveMode.ReceiveAndDelete });

// Receives a message
var sbReceivedMessageObjectWithReceiveAndDelete = await sbReceiverObjectReceiveAndDelete.ReceiveMessageAsync();
var msgObjectWithReceiveAndDelete = sbReceivedMessageObjectWithReceiveAndDelete.Body.ToString();

// Deserialize into an Object
var empInObjWithReceiveAndDelete = JsonSerializer.Deserialize<Employee>(msgObjectWithReceiveAndDelete);

// Display the name in console.
Console.WriteLine($"Message what we received and emp Name is : {empInObjWithReceiveAndDelete?.Name}");

#endregion

#region ServiceBus_SendMessage_DuplicateMessagesRestriction

// Create a ServiceBusSender
sbSender = sb.CreateSender("<Add_Your_Queue_Or_TopicName");

// Prepare a message
string message = "Two times created";
var sbMessage1 = new ServiceBusMessage(message);
var sbMessage2 = new ServiceBusMessage(message);

// Send a message(s) => Two times two entries added in queue
await sbSender.SendMessageAsync(sbMessage1);
await sbSender.SendMessageAsync(sbMessage2);


string duplicateMessageRestriction = "One time create";
var sbMessage3 = new ServiceBusMessage(duplicateMessageRestriction);
sbMessage3.MessageId = "123";

var sbMessage4 = new ServiceBusMessage(duplicateMessageRestriction);
sbMessage3.MessageId = "123";

// Send a message(s) => Only one entry because for both messages having same messageId
await sbSender.SendMessageAsync(sbMessage3);
await sbSender.SendMessageAsync(sbMessage4);

Console.WriteLine("Demos on Azure Servicebus - End");

#endregion

#region ServiceBus_Cross_Entity_Transactions
// Create a client with transactions option as true

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

#region ServiceBusTopic_With_No_Filters

// Ex: While sending a message from c#
// All code is same while sending a message only difference is instead of queuename we should pass topic name

#endregion

#region ServiceBusTopic_Read_Messages_From_Subscriptions

// All receiveer logic is same only one difference is while reading we should pass both topic and subscription
var sbReceiverTopic_with_subscription = sbClient.CreateReceiver("<Topic_Name>", "<SubscriptionName");

// Reading
await sbReceiverTopic_with_subscription.ReceiveMessageAsync();

#endregion

#region CreateNewSubscriber_For_Topic

// Create a new object using ServiceBusAdministrationClient
// This one comes from this namespace:using Azure.Messaging.ServiceBus.Administration;
var sbAdminClient = new ServiceBusAdministrationClient("Connectionstring");

// Create a new subscriber
await sbAdminClient.CreateSubscriptionAsync("<your_topic_name", "<Your_new_subscription_name>");

// For queue/topic also same
await sbAdminClient.CreateTopicAsync("<your_topic_name>");
await sbAdminClient.CreateQueueAsync("<Your_queue_name>");

Console.WriteLine("Creation of subscription is successfull");
#endregion
public class Employee
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
}
