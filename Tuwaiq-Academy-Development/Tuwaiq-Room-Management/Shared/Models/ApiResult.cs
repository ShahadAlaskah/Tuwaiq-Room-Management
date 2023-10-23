using Shared.Domain;

namespace Shared.Models;

public class ApiResult<T> : IBaseNotificationServiceDto where T : class
{
    public int? Page { get; set; } = 1;
    public int? PageCount { get; set; } = 10;
    public string? Query { get; set; } = null!;
    public List<T>? Data { get; set; } = null!;
    public bool HasNext { get; set; }
    public bool HasPrev { get; set; }
    public int TotalPages { get; set; }
    public int TotalCount { get; set; }
}