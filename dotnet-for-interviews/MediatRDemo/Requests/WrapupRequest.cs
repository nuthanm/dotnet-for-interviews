using MediatR;

namespace MediatRDemo.Requests
{
    public record WrapupRequest(string wrapUpRequest) : IRequest;
}
