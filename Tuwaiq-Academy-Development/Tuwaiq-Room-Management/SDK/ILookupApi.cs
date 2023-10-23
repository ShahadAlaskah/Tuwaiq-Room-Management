using Refit;
using SDK.Core;


namespace SDK;

[Headers("Authorization: Bearer", "Accept: application/json")]
public interface ILookupApi
{
    [Get("/Lookups/GetAssetTypes")]
    Task<IApiResponse<List<LookupDictionary>>> GetAssetTypes();
    [Get("/Lookups/GetBuildings")]
    Task<IApiResponse<List<LookupDictionary>>> GetBuildings();
    [Get("/Lookups/GetFloors")]
    Task<IApiResponse<List<LookupDictionary>>> GetFloors();
    [Get("/Lookups/GetRoomTypes")]
    Task<IApiResponse<List<LookupDictionary>>> GetRoomTypes();
    [Get("/Lookups/GetRooms")]
    Task<IApiResponse<List<LookupDictionary>>> GetRooms();
//     [Get("/Lookups/Search/{pageSize}/{page}")]
//     Task<IApiResponse<SearchResponse>> Search(string query, int? page = 1, int? pageSize = 10);
 }