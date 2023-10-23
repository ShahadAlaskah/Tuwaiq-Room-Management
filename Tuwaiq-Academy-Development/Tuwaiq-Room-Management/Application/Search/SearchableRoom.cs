using Application.Dto;

namespace Application.Search;

public class SearchableRoom : Searchable
{
    public SearchableRoom(RoomDto item, IViewRenderService viewRenderer)
    {
        var ids = item.Id;
        var dic = new List<string>();
        dic.Add(string.Join(", ", ids));
        var descriptionPath = $"Client/Room/Room/{item.Id}";
        DescriptionPath = descriptionPath;
        Description = viewRenderer.RenderToStringAsync("SearchViews/RoomSearch", item).Result;
        Href = $"/Client/Room/Room/{item.Id}";
        Id = "Room_" + item.Id.ToString();
        Title = $"{item.Code} {item.Name} {string.Join(", ", dic.Where(s => !string.IsNullOrEmpty(s)))}";
    }

    public override string Description { get; }
    public override string DescriptionPath { get; }
    public override string Href { get; }
    public override string Id { get; }
    public override string Title { get; }
}