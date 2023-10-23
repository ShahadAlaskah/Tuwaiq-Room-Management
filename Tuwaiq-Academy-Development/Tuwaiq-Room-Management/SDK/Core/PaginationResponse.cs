namespace SDK.Core;

public class PaginationResponse<T> where T : class, new()
{
    public Pagination Pagination { get; set; } = null!;

    public List<T> Data { get; set; } = null!;
    // public Links Links { get; set; }
}