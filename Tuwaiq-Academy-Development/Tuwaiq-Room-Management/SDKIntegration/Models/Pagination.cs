using Tapper;

namespace SDKIntegration.Models;

[TranspilationSource]
public class Pagination
{
    public int Offset { get; set; }
    public int Limit { get; set; }
    public int Total { get; set; }
    public int TotalPages { get; set; }
}