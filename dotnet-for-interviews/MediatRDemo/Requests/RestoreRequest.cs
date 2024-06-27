using MediatR;

namespace MediatRDemo.Requests
{
    public record RestoreRequest(string restoreRequest) : IRequest;
}
