using MediatR;
using MediatRDemo.Models;
using MediatRDemo.Requests;
using System.Text.Json;

namespace MediatRDemo.RequestHandlers
{
    public class WrapupRequestHandler : IRequestHandler<WrapupRequest>
    {
        public WrapupRequestHandler()
        {
            // Inject service or repository or any other dependencies
            // access those in Handle method
        }
        public Task Handle(WrapupRequest request, CancellationToken cancellationToken)
        {
            var wrapUpRequest = JsonSerializer.Deserialize<Wrapup>(request.wrapUpRequest);
            if (wrapUpRequest != null)
            {
                foreach (var item in wrapUpRequest.Payloads)
                {
                    Console.WriteLine($"Request Id:{item.RequestId} and is it wrapped up or not: {item.IsWrapup}");

                    if (item.IsWrapup)
                    {
                        // Process here
                    }
                }
                // Write your own db logic example to update your delete entries
            }
            return Task.CompletedTask;
        }
    }
}
