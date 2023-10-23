using MediatR;
using Shared.Base;

namespace Shared.Interfaces;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}