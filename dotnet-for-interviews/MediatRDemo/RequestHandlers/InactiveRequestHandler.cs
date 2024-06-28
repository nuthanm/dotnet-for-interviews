using MediatR;
using MediatRDemo.Models;
using MediatRDemo.Requests;
using System.Text.Json;

namespace MediatRDemo.RequestHandlers
{
    public class InactiveRequestHandler : IRequestHandler<InactiveRequest>
    {
        public InactiveRequestHandler()
        {
            // Inject service or repository or any other dependencies
            // access those in Handle method
        }
        public Task Handle(InactiveRequest request, CancellationToken cancellationToken)
        {
            var inactiveRequest = JsonSerializer.Deserialize<Request<InActivePayLoad>>(request.inactiveRequest);

            if (inactiveRequest != null)
            {
                foreach (var item in inactiveRequest.Payloads)
                {
                    Console.WriteLine($"Request Id:{item.RequestId} and is it inactive: {item.IsInactive}");

                    // Write your own db logic example to update your delete entries
                }
            }

            return Task.CompletedTask;
        }
    }
}
