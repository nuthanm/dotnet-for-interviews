using MediatR;

namespace MediatRDemo.Requests
{
    public record DeleteRequest(string deleteRequest) : IRequest;
}
