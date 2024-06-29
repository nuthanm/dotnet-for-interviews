﻿// See https://aka.ms/new-console-template for more information
using Azure.Messaging.ServiceBus;
using System.Text.Json;


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
Console.WriteLine($"Message what we received and emp Name is : {empInObj.Name}");

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
Console.WriteLine($"Message what we received and emp Name is : {empInObj.Name}");

#endregion
public class Employee
{
    public int Id { get; set; }

    public string Name { get; set; }
}
