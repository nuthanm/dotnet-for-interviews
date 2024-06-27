using MediatR;

namespace MediatRDemo.Requests
{
    public record InactiveRequest(string inactiveRequest) : IRequest;
}
