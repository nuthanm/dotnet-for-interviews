using MediatR;

namespace MediatRDemo.Requests
{
    public record LegalholdRequest(string legalHoldRequest) : IRequest;
}
