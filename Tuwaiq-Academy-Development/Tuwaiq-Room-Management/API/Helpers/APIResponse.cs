namespace API.Helpers;

public class ApiResponse<T> where T : class, new()
{
    public Pagination Pagination { get; set; } = null!;

    public List<T> Data { get; set; } = null!;
    // public Links Links { get; set; }
}

// public class Links
// {
//     public string First { get; set; }
//     public string Prev { get; set; }
//     public string Next { get; set; }
//     public string Last { get; set; }
// }

public class Pagination
{
    public int Offset { get; set; }
    public int Limit { get; set; }
    public int Total { get; set; }
    public int TotalPages { get; set; }
}