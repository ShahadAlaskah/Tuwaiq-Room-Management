namespace SDKIntegration.Models;

public class PaginationResponse<T> where T : class, new()
{
    public Pagination Pagination { get; set; } = null!;

    public List<T> Data { get; set; } = null!;
}