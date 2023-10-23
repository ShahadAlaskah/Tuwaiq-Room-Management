using Shared.Interfaces;
using Shared.Models;

namespace Application.Persistence.Queries;

public class GetLookupQuery : IQuery<PaginatedList<LookupDictionary>>
{
    public GetLookupQuery(Type type, int? page=null, int? pageCount=null)
    {
        Entity = type;
        Page = page;
        PageCount = pageCount;
    }

    public Type Entity { get; set; }
    public int? Page { get; set; } = 1;
    public int? PageCount { get; set; } = 10;
}