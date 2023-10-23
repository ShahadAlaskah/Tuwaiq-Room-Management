using Application;
using Application.Commands.AuditViews.Commands;
using Application.Interfaces;
using Application.Persistence.Queries;
using Domain.Base;
using MediatR;
using Microsoft.AspNetCore.Http;
using Shared.Base;
using Shared.Enums;
using Shared.Extensions;
using Shared.Interfaces;

namespace Infrastructure.Persistence.QueryHandlers;

public class GetAuditQueryHandler : IQueryHandler<GetAuditQuery, PaginatedList<Audit>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly HttpContext? _httpContext;
    private readonly ISender? _sender;

    public GetAuditQueryHandler(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor, ISender? sender)
    {
        _httpContext = contextAccessor.HttpContext;
        _unitOfWork = unitOfWork;
        _sender = sender;
    }

    public async Task<Result<PaginatedList<Audit>>> Handle(GetAuditQuery request, CancellationToken cancellationToken)
    {
        await _sender.Send(new CreateAuditViewCommand(AuditViewType.View, _httpContext?.User.GetUserId(), "Query Audits", new[] { "Audits" }, new Dictionary<string, object>
                { { "Page", request.Page ?? 1 }, { "PageCount", request.PageCount ?? 10 } }), cancellationToken);

        return Result.Ok(await PaginatedList<Audit>.CreateAsync(
            _unitOfWork.Audits!.FindWithSpecificationPattern(request.Specification), request.Page ?? 1,
            request.PageCount ?? 10));
    }
}