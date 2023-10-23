using Refit;
using SDK.Core;
using SDK.Core.Commands;
using SDK.Core.Models;

namespace SDK;

[Headers("Authorization: Bearer", "Accept: application/json")]
public interface IBuildingsApi
{
    [Get("/Buildings/Get")]
    Task<ApiResponse<PaginationResponse<BuildingDto>>> Get(int? page = 1, int? pageSize = 10, string? query = null);

    [Get("/Buildings/Get/{id}")]
    Task<IApiResponse<BuildingDto>> GetById(Guid id);

    [Post("/Buildings/Post")]
    Task<IApiResponse<Guid>> Create(CreateBuildingCommand command);

    [Put("/Buildings/Put")]
    Task<IApiResponse<Guid>> Update(UpdateBuildingCommand command);

    [Delete("/Buildings/Delete/{id}")]
    Task<IApiResponse<Guid>> Delete(Guid id);
}