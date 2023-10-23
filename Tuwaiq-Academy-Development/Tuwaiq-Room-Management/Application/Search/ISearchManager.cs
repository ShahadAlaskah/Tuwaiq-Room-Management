namespace Application.Search;

public interface ISearchManager
{
    void AddToIndex(params Searchable[] searchables);
    void DeleteFromIndex(params Searchable[] searchables);
    void Clear();
    void InitAll();
    SearchResultCollection Search(string searchQuery, int hitsStart, int hitsStop, string[] fields);
}