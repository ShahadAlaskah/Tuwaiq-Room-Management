using Application.Dto;

namespace Application.Search;

public class SearchableAsset : Searchable
{
    public SearchableAsset(AssetDto item, IViewRenderService viewRenderer)
    {
        var ids = item.Id;
        var dic = new List<string>();
        dic.Add(string.Join(", ", ids));
        var descriptionPath = $"Client/Asset/Asset/{item.Id}";
        DescriptionPath = descriptionPath;
        Description = viewRenderer.RenderToStringAsync("SearchViews/AssetSearch", item).Result;
        Href = $"/Client/Asset/Asset/{item.Id}";
        Id = "Asset_" + item.Id.ToString();
        Title = $"{item.Code} {string.Join(", ", dic.Where(s => !string.IsNullOrEmpty(s)))}";
    }

    public override string Description { get; }
    public override string DescriptionPath { get; }
    public override string Href { get; }
    public override string Id { get; }
    public override string Title { get; }
}