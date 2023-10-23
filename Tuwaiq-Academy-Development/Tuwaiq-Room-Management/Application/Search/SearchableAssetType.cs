using Application.Dto;

namespace Application.Search;

public class SearchableAssetType : Searchable
{
    public SearchableAssetType(AssetTypeDto item, IViewRenderService viewRenderer)
    {
        var ids = item.Id;
        var dic = new List<string>();
        dic.Add(string.Join(", ", ids));
        var descriptionPath = $"Client/AssetType/AssetType/{item.Id}";
        DescriptionPath = descriptionPath;
        Description = viewRenderer.RenderToStringAsync("SearchViews/AssetTypeSearch", item).Result;
        Href = $"/Client/AssetType/AssetType/{item.Id}";
        Id = "AssetType_" + item.Id.ToString();
        Title = $"{item.Name} {string.Join(", ", dic.Where(s => !string.IsNullOrEmpty(s)))}";
    }

    public override string Description { get; }
    public override string DescriptionPath { get; }
    public override string Href { get; }
    public override string Id { get; }
    public override string Title { get; }
}