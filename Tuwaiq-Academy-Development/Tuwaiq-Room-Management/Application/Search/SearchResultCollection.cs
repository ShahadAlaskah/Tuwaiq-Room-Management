namespace Application.Search;

public class SearchResultCollection
{
    private List<SearchResult> _data = null!;

    public int Count { get; set; }

    public List<SearchResult> Data
    {
        get => _data;
        set => _data = value;
    }
}