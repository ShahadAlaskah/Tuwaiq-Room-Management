using Refit;
using SDK.Core;
using SDK.Core.Commands;
using SDK.Core.Models;

namespace SDK;

[Headers("Authorization: Bearer", "Accept: application/json")]
public interface IFloorsApi
{
    [Get("/Floors/Get")]
    Task<ApiResponse<PaginationResponse<FloorDto>>> Get(int? page = 1, int? pageSize = 10, string? query = null);

    [Get("/Floors/Get/{id}")]
    Task<IApiResponse<FloorDto>> GetById(Guid id);

    [Post("/Floors/Post")]
    Task<IApiResponse<Guid>> Create(CreateFloorCommand command);

    [Put("/Floors/Put")]
    Task<IApiResponse<Guid>> Update(UpdateFloorCommand command);

    [Delete("/Floors/Delete/{id}")]
    Task<IApiResponse<Guid>> Delete(Guid id);
}