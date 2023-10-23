namespace Shared.Models;

public class ApiRequest
{
    public ApiRequest()
    {
        if (PageCount <= 0) PageCount = 10;
        if (Page <= 0) Page = 1;
    }

    public int? Page { get; set; } = 1;
    public int? PageCount { get; set; } = 10;
    public string? Query { get; set; } = null!;
}