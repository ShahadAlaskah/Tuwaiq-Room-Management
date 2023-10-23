using Refit;
using SDK.Core;
using SDK.Core.Commands;
using SDK.Core.Models;

namespace SDK;

[Headers("Authorization: Bearer", "Accept: application/json")]
public interface IRoomTypesApi
{
    [Get("/RoomTypes/Get")]
    Task<ApiResponse<PaginationResponse<RoomTypeDto>>> Get(int? page = 1, int? pageSize = 10, string? query = null);

    [Get("/RoomTypes/Get/{id}")]
    Task<IApiResponse<RoomTypeDto>> GetById(Guid id);

    [Post("/RoomTypes/Post")]
    Task<IApiResponse<Guid>> Create(CreateRoomTypeCommand command);

    [Put("/RoomTypes/Put")]
    Task<IApiResponse<Guid>> Update(UpdateRoomTypeCommand command);

    [Delete("/RoomTypes/Delete/{id}")]
    Task<IApiResponse<Guid>> Delete(Guid id);
}