using Refit;
using SDK.Core;
using SDK.Core.Commands;
using SDK.Core.Models;

namespace SDK;

[Headers("Authorization: Bearer", "Accept: application/json")]
public interface IRoomsApi
{
    [Get("/Rooms/Get")]
    Task<ApiResponse<PaginationResponse<RoomDto>>> Get(int? page = 1, int? pageSize = 10, string? query = null);

    [Get("/Rooms/GetAssets/{id}")]
    Task<IApiResponse<ApiResponse<AssetDto>>> GetAssets(Guid id);

    [Get("/Rooms/Get/{id}")]
    Task<IApiResponse<RoomDto>> GetById(Guid id);

    [Post("/Rooms/Post")]
    Task<IApiResponse<Guid>> Create(CreateRoomCommand command);

    [Put("/Rooms/Put")]
    Task<IApiResponse<Guid>> Update(UpdateRoomCommand command);

    [Delete("/Rooms/Delete/{id}")]
    Task<IApiResponse<Guid>> Delete(Guid id);
    
    [Put("/Rooms/MarkRoomAsUnderMaintenance/{id}")]
    Task<IApiResponse<Guid>> MarkRoomAsUnderMaintenance(Guid id);
    
    [Put("/Rooms/MarkRoomAsAvailable/{id}")]
    Task<IApiResponse<Guid>> MarkRoomAsAvailable(Guid id);

    [Post("/Rooms/AddAssetToRoom")]
    Task<IApiResponse<Guid>> AddAssetToRoom(AddAssetToRoomCommand command);

    [Delete("/Rooms/RemoveAssetFromRoom/{id}/{assetId}")]
    Task<IApiResponse<Guid>> RemoveAssetFromRoom(Guid id, Guid assetId);

}