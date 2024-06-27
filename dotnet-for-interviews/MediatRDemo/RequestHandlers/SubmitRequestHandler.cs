using MediatR;
using MediatRDemo.Models;
using MediatRDemo.Requests;
using System.Text.Json;

namespace MediatRDemo.RequestHandlers
{
    public class SubmitRequestHandler : IRequestHandler<SubmitRequest>
    {
        public SubmitRequestHandler()
        {
            // Inject service or repository or any other dependencies
            // access those in Handle method
        }
        public Task Handle(SubmitRequest request, CancellationToken cancellationToken)
        {
            var submitRequest = JsonSerializer.Deserialize<Submit>(request.submitRequest);
            if (submitRequest != null)
            {
                Console.WriteLine($"Request Id:{submitRequest.RequestId} and is it requestProcess: {submitRequest.IsRequestProcess} and is it new/reiniate:{submitRequest.IsReinitate}");

                if (submitRequest.IsRequestProcess)
                {
                    // Process new request or else do other implementation.
                }

                // Write your own db logic example to update your delete entries
            }
            return Task.CompletedTask;
        }
    }
}
