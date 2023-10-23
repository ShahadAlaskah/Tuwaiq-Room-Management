using Application.Interfaces;
using Domain.Base;
using Shared.Interfaces;

namespace Application.Persistence.Queries;

public class GetAuditQuery : IQuery<PaginatedList<Audit>>
{
    public GetAuditQuery(ISpecification<Audit> specification, int? page, int? pageCount)
    {
        Specification = specification;
        Page = page;
        PageCount = pageCount;
    }

    public ISpecification<Audit> Specification { get; set; }
    public int? Page { get; set; } = 1;
    public int? PageCount { get; set; } = 10;
}