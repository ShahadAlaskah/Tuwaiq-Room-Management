using Tapper;

namespace SDK.Core;

// public class Links
// {
//     public string First { get; set; }
//     public string Prev { get; set; }
//     public string Next { get; set; }
//     public string Last { get; set; }
// }

[TranspilationSource]
public class Pagination
{
    public int Offset { get; set; }
    public int Limit { get; set; }
    public int Total { get; set; }
    public int TotalPages { get; set; }
}