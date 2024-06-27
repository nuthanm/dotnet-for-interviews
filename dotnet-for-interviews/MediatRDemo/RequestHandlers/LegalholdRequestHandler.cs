using MediatR;
using MediatRDemo.Models;
using MediatRDemo.Requests;
using System.Text.Json;

namespace MediatRDemo.RequestHandlers
{
    public class LegalholdRequestHandler : IRequestHandler<LegalholdRequest>
    {
        public LegalholdRequestHandler()
        {
            // Inject service or repository or any other dependencies
            // access those in Handle method
        }
        public Task Handle(LegalholdRequest request, CancellationToken cancellationToken)
        {
            var legalHoldRequest = JsonSerializer.Deserialize<LegalHold>(request.legalHoldRequest);
            if (legalHoldRequest != null)
            {
                foreach (var item in legalHoldRequest.Payloads)
                {
                    Console.WriteLine($"Request Id:{item.RequestId} and is it legalhold: {item.IsLegalHold} or is it Lifted: {item.IsLifted}");
                }

                // Write your own db logic example to update your delete entries
            }
            return Task.CompletedTask;
        }
    }
}
