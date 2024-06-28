using MediatR;
using MediatRDemo.Models;
using MediatRDemo.Requests;
using System.Text.Json;

namespace MediatRDemo.RequestHandlers
{
    public class RestoreRequestHandler : IRequestHandler<RestoreRequest>
    {
        public RestoreRequestHandler()
        {
            // Inject service or repository or any other dependencies
            // access those in Handle method
        }
        public Task Handle(RestoreRequest request, CancellationToken cancellationToken)
        {

            var restoreRequest = JsonSerializer.Deserialize<Request<RestorePayLoad>>(request.restoreRequest);
            if (restoreRequest != null)
            {
                foreach (var item in restoreRequest.Payloads)
                {
                    Console.WriteLine($"Request Id:{item.RequestId} and is it Archive or not: {item.IsArchive}");
                }

                // Write your own db logic example to update your delete entries
            }
            return Task.CompletedTask;
        }
    }
}
