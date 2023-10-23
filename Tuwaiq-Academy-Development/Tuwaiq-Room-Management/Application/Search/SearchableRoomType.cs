using Application.Dto;

namespace Application.Search;

public class SearchableRoomType: Searchable
{
    public SearchableRoomType(RoomTypeDto item, IViewRenderService viewRenderer)
    {
        var ids = item.Id;
        var dic = new List<string>();
        dic.Add(string.Join(", ", ids));
        var descriptionPath = $"Client/RoomType/RoomType/{item.Id}";
        DescriptionPath = descriptionPath;
        Description = viewRenderer.RenderToStringAsync("SearchViews/RoomTypeSearch", item).Result;
        Href = $"/Client/RoomType/RoomType/{item.Id}";
        Id = "RoomType_" + item.Id.ToString();
        Title = $"{item.Name} {string.Join(", ", dic.Where(s => !string.IsNullOrEmpty(s)))}";
    }

    public override string Description { get; }
    public override string DescriptionPath { get; }
    public override string Href { get; }
    public override string Id { get; }
    public override string Title { get; }
}