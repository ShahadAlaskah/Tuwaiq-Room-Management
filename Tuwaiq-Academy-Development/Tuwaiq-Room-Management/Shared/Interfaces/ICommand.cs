using MediatR;
using Shared.Base;

namespace Shared.Interfaces;

public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}