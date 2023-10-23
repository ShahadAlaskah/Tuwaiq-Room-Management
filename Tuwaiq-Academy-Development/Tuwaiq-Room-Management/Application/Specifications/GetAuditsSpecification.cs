using Application.Handlers;
using Domain.Base;

namespace Application.Specifications;

public class GetAuditsSpecification : BaseSpecification<Audit>
{
    public GetAuditsSpecification(string? query = null) : base(item => string.IsNullOrEmpty(query)
                                                                       || item.NewValues!.ToLower().Contains(query.ToLower())
                                                                       || item.OldValues!.ToLower().Contains(query.ToLower())
                                                                       || item.Type.ToLower().Contains(query.ToLower())
                                                                       || item.TableName.ToLower().Contains(query.ToLower())
    )
    {
        AddOrderBy(s => s.Id);
    }
}