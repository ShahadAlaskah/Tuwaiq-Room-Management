namespace Application.Search;

public interface IViewRenderService
{
    Task<string> RenderToStringAsync(string viewName, object model);
}