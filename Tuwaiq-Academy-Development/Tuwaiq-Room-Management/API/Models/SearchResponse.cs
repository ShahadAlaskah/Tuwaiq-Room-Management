namespace API.Models;

public class SearchResponse
{
    public string Text { get; set; } = null!;
    public SearchResponseChild[] Children { get; set; } = null!;
}

public class SearchResponseChild
{
    public string Id { get; set; } = null!;
    public string Text { get; set; } = null!;
    public string Link { get; set; } = null!;
}