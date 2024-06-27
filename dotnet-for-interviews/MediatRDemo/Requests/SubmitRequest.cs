using MediatR;

namespace MediatRDemo.Requests
{
    public record SubmitRequest(string submitRequest) : IRequest;
}
