using MediatR;
using MediatRDemo.Models;
using MediatRDemo.Requests;
using System.Text.Json;

namespace MediatRDemo.RequestHandlers
{
    public class DeleteRequestHandler : IRequestHandler<DeleteRequest>
    {
        public DeleteRequestHandler()
        {
            // Inject service or repository or any other dependencies
            // access those in Handle method
        }
        public Task Handle(DeleteRequest request, CancellationToken cancellationToken)
        {
            var deleteRequest = JsonSerializer.Deserialize<Request<DeletePayLoad>>(request.deleteRequest);
            if (deleteRequest != null)
            {
                foreach (var item in deleteRequest.Payloads)
                {
                    Console.WriteLine($"Request Id:{item.RequestId} and is it deleted: {item.IsDeleted}");
                }

                // Write your own db logic example to update your delete entries
            }
            return Task.CompletedTask;
        }
    }
}
