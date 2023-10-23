using Application.Handlers;
using Domain.Base;

namespace Application.Specifications;

public class GetAuditViewsSpecification : BaseSpecification<AuditView>
{
    public GetAuditViewsSpecification(string? query = null) : base(item => string.IsNullOrEmpty(query) || item.Description!.ToLower().Contains(query.ToLower())
    )
    {
        AddOrderBy(s => s.Id);
    }
}