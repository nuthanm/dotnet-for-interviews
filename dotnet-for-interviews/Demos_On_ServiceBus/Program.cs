// See https://aka.ms/new-console-template for more information
using Azure.Messaging.ServiceBus;


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