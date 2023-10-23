using Refit;
using SDK.Core;
using SDK.Core.Commands;
using SDK.Core.Models;

namespace SDK;

[Headers("Authorization: Bearer", "Accept: application/json")]
public interface IAssetTypesApi
{
    [Get("/AssetTypes/Get")]
    Task<ApiResponse<PaginationResponse<AssetTypeDto>>> Get(int? page = 1, int? pageSize = 10, string? query = null);

    [Get("/AssetTypes/Get/{id}")]
    Task<IApiResponse<AssetTypeDto>> GetById(Guid id);

    [Post("/AssetTypes/Post")]
    Task<IApiResponse<Guid>> Create(CreateAssetTypeCommand command);

    [Put("/AssetTypes/Put")]
    Task<IApiResponse<Guid>> Update(UpdateAssetTypeCommand command);

    [Delete("/AssetTypes/Delete/{id}")]
    Task<IApiResponse<Guid>> Delete(Guid id);
}