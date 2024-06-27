// See https://aka.ms/new-console-template for more information
using MediatR;
using MediatRDemo.Requests;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

/*
 * If you are adding in BAL or in azure function or webjob
 * builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
});
 */

using IHost host = builder.Build();

var mediator = host.Services.GetService<IMediator>();

Console.WriteLine("Hello, World!");

// For Submitted
var submitted = @"{
  ""EventType"":""Submitted"",
  ""Payload"": {
    ""RequestId"": 1024,
    ""IsReinitiate"": false,
    ""IsRequestProcess"": true,
    ""BlobUrl"":""https://url.for.extract.com""
  }
}";

// For Re-Initiate
var reInitiate = @"{
  ""EventType"":""Submitted"",
  ""Payload"": {
    ""RequestId"": 1024,
    ""IsReinitiate"": true,
    ""IsRequestProcess"": true,
    ""BlobUrl"":""https://url.for.extract.com""
  }
}";

// For Delete

var delete = @"{
  ""EventType"":""Delete"",
  ""Payloads"": [
    {
        ""RequestId"": 1024,
        ""IsDeleted"": true
    }
  ]
}";


// For Legalhold - Initiate
var legalInitiate = @"{
  ""EventType"":""LegalHold"",
  ""Payloads"": [
    {
       ""RequestId"": 1024,
       ""IsLegalHold"": true,
       ""IsLifted"": false
    },
    {
       ""RequestId"": 1025,
       ""IsLegalHold"": true,
       ""IsLifted"": false
    }
  ]
}";

// For Legalhold - Lifted
var legalLifted = @"{
  ""EventType"":""LegalHold"",
  ""Payloads"": [
    {
       ""RequestId"": 1024,
       ""IsLegalHold"": false,
       ""IsLifted"": true
    },
    {
       ""RequestId"": 1025,
       ""IsLegalHold"": false,
       ""IsLifted"": true
    }
  ]
}";

// For WrapUp
var wrapup = @"{
  ""EventType"":""Wrapup"",
  ""Payloads"": [
    {
       ""RequestId"": 1024,
       ""IsWrapup"": true
    },
    {
       ""RequestId"": 1025,
       ""IsWrapup"": true
    }
  ]
}";

// For Inactive
var inActive = @"{
  ""EventType"":""InActive"",
  ""Payloads"": [
    {
       ""RequestId"": 1024,
       ""IsInActive"": true
    },
    {
       ""RequestId"": 1025,
       ""IsInActive"": true
    }
  ]
}";

// For Restore
var restore = @"{
  ""EventType"":""Restore"",
  ""Payloads"": [
    {
       ""RequestId"": 1024,
       ""IsArchive"": false
    },
    {
       ""RequestId"": 1025,
       ""IsArchive"": false
    }
  ]
}";


// Step 1: Parse Input Json
// Step 2: Get EventType value
// Step 3: Based on eventType we should parse data model accordingly or call that handler and process the record using meditr pattern

//JObject jObject = JObject.Parse(jsonString);
//string eventType = (string)jObject["EventType"];
//Console.WriteLine($"Event Type: {eventType}");

using (var doc = JsonDocument.Parse(legalLifted))
{
    var root = doc.RootElement;
    Console.WriteLine($"Event Type: {root.GetProperty("EventType").GetString()}");
    switch (root.GetProperty("EventType").GetString())
    {
        case "Submitted":
            await mediator.Send(new SubmitRequest(submitted));
            break;

        case "Delete":
            await mediator.Send(new DeleteRequest(delete));
            break;


        case "InActive":
            await mediator.Send(new InactiveRequest(inActive));
            break;


        case "Restore":
            await mediator.Send(new RestoreRequest(restore));
            break;


        case "Wrapup":
            await mediator.Send(new WrapupRequest(wrapup));
            break;

        case "LegalHold":
            await mediator.Send(new LegalholdRequest(legalLifted));
            break;

        default: throw new Exception("No matching request");
    }
}



