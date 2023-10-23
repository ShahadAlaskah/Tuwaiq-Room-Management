using API.Helpers;
using API.Models;
using Application;
using Application.Persistence.Queries;
using Application.Search;
using Domain.Domains;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Validation.AspNetCore;
using Shared.Interfaces.ValidationErrors;
using Shared.Models;
using Shared.Base;


namespace API.Controllers;

[Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
[Route("[controller]")]
[ApiController]
public class LookupsController : ControllerBase
{
    private readonly ISender _sender;
    private readonly ISearchManager _searchManager;

    public LookupsController(ISender sender, ISearchManager searchManager)
    {
        _sender = sender;
        _searchManager = searchManager;
    }

    [ProducesResponseType(typeof(ApiResponse<LookupDictionary>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [HttpGet("[action]")]
    public async Task<IActionResult> GetAssetTypes()
    {
        Result<PaginatedList<LookupDictionary>> result = await _sender.Send(new GetLookupQuery(typeof(AssetType), 1, 10000));

        if (result.Failure) return BadRequest(result.Error);

        Response.Headers.Add("firstPage",
            result.Value.HasPreviousPage ? $"{Url.Action("GetAssetTypes", new { page = 1, pageSize = 1 })}" : "");
        Response.Headers.Add("prevPage",
            result.Value.HasPreviousPage ? $"{Url.Action("GetAssetTypes", new { page = 1, pageSize = 1 })}" : "");
        Response.Headers.Add("nextPage",
            result.Value.HasNextPage ? $"{Url.Action("GetAssetTypes", new { page = 1, pageSize = 1 })}" : "");
        Response.Headers.Add("lastPage", result.Value.HasNextPage
            ? $"{Url.Action("GetAssetTypes", new { page = result.Value.TotalPages, pageSize = 1 })}"
            : "");

        return Ok(new ApiResponse<LookupDictionary>
        {
            Pagination = new()
            {
                Offset = (1) + (10000),
                Limit = 10000,
                Total = result.Value.TotalCount,
                TotalPages = result.Value.TotalPages
            },
            Data = result.Value.ToList(),
        });
    }

    [ProducesResponseType(typeof(ApiResponse<LookupDictionary>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [HttpGet("[action]")]
    public async Task<IActionResult> GetBuildings()
    {
        Result<PaginatedList<LookupDictionary>> result = await _sender.Send(new GetLookupQuery(typeof(Building), 1, 10000));

        if (result.Failure) return BadRequest(result.Error);

        Response.Headers.Add("firstPage",
            result.Value.HasPreviousPage ? $"{Url.Action("GetBuildings", new { page = 1, pageSize = 1 })}" : "");
        Response.Headers.Add("prevPage",
            result.Value.HasPreviousPage ? $"{Url.Action("GetBuildings", new { page = 1, pageSize = 1 })}" : "");
        Response.Headers.Add("nextPage",
            result.Value.HasNextPage ? $"{Url.Action("GetBuildings", new { page = 1, pageSize = 1 })}" : "");
        Response.Headers.Add("lastPage", result.Value.HasNextPage
            ? $"{Url.Action("GetBuildings", new { page = result.Value.TotalPages, pageSize = 1 })}"
            : "");

        return Ok(new ApiResponse<LookupDictionary>
        {
            Pagination = new()
            {
                Offset = (1) + (10000),
                Limit = 10000,
                Total = result.Value.TotalCount,
                TotalPages = result.Value.TotalPages
            },
            Data = result.Value.ToList(),
        });
    }

    [ProducesResponseType(typeof(ApiResponse<LookupDictionary>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [HttpGet("[action]")]
    public async Task<IActionResult> GetFloors()
    {
        Result<PaginatedList<LookupDictionary>> result = await _sender.Send(new GetLookupQuery(typeof(Floor), 1, 10000));

        if (result.Failure) return BadRequest(result.Error);

        Response.Headers.Add("firstPage",
            result.Value.HasPreviousPage ? $"{Url.Action("GetFloors", new { page = 1, pageSize = 1 })}" : "");
        Response.Headers.Add("prevPage",
            result.Value.HasPreviousPage ? $"{Url.Action("GetFloors", new { page = 1, pageSize = 1 })}" : "");
        Response.Headers.Add("nextPage",
            result.Value.HasNextPage ? $"{Url.Action("GetFloors", new { page = 1, pageSize = 1 })}" : "");
        Response.Headers.Add("lastPage", result.Value.HasNextPage
            ? $"{Url.Action("GetFloors", new { page = result.Value.TotalPages, pageSize = 1 })}"
            : "");

        return Ok(new ApiResponse<LookupDictionary>
        {
            Pagination = new()
            {
                Offset = (1) + (10000),
                Limit = 10000,
                Total = result.Value.TotalCount,
                TotalPages = result.Value.TotalPages
            },
            Data = result.Value.ToList(),
        });
    }

    [ProducesResponseType(typeof(ApiResponse<LookupDictionary>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [HttpGet("[action]")]
    public async Task<IActionResult> GetRoomTypes()
    {
        Result<PaginatedList<LookupDictionary>> result = await _sender.Send(new GetLookupQuery(typeof(RoomType), 1, 10000));

        if (result.Failure) return BadRequest(result.Error);

        Response.Headers.Add("firstPage",
            result.Value.HasPreviousPage ? $"{Url.Action("GetRoomTypes", new { page = 1, pageSize = 1 })}" : "");
        Response.Headers.Add("prevPage",
            result.Value.HasPreviousPage ? $"{Url.Action("GetRoomTypes", new { page = 1, pageSize = 1 })}" : "");
        Response.Headers.Add("nextPage",
            result.Value.HasNextPage ? $"{Url.Action("GetRoomTypes", new { page = 1, pageSize = 1 })}" : "");
        Response.Headers.Add("lastPage", result.Value.HasNextPage
            ? $"{Url.Action("GetRoomTypes", new { page = result.Value.TotalPages, pageSize = 1 })}"
            : "");

        return Ok(new ApiResponse<LookupDictionary>
        {
            Pagination = new()
            {
                Offset = (1) + (10000),
                Limit = 10000,
                Total = result.Value.TotalCount,
                TotalPages = result.Value.TotalPages
            },
            Data = result.Value.ToList(),
        });
    }

    [ProducesResponseType(typeof(ApiResponse<LookupDictionary>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [HttpGet("[action]")]
    public async Task<IActionResult> GetRooms()
    {
        Result<PaginatedList<LookupDictionary>> result = await _sender.Send(new GetLookupQuery(typeof(Room), 1, 10000));

        if (result.Failure) return BadRequest(result.Error);

        Response.Headers.Add("firstPage",
            result.Value.HasPreviousPage ? $"{Url.Action("GetRooms", new { page = 1, pageSize = 1 })}" : "");
        Response.Headers.Add("prevPage",
            result.Value.HasPreviousPage ? $"{Url.Action("GetRooms", new { page = 1, pageSize = 1 })}" : "");
        Response.Headers.Add("nextPage",
            result.Value.HasNextPage ? $"{Url.Action("GetRooms", new { page = 1, pageSize = 1 })}" : "");
        Response.Headers.Add("lastPage", result.Value.HasNextPage
            ? $"{Url.Action("GetRooms", new { page = result.Value.TotalPages, pageSize = 1 })}"
            : "");

        return Ok(new ApiResponse<LookupDictionary>
        {
            Pagination = new()
            {
                Offset = (1) + (10000),
                Limit = 10000,
                Total = result.Value.TotalCount,
                TotalPages = result.Value.TotalPages
            },
            Data = result.Value.ToList(),
        });
    }

    [ProducesResponseType(typeof(SearchResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [HttpGet("[action]/{pageSize}/{page}")]
    public IActionResult Search(string query, int? page = 1, int? pageSize = 10)
    {
        if (string.IsNullOrEmpty(query)) return Ok("");

        var search = _searchManager.Search(query, 0, 100, Searchable.AnalyzedFields.Values.ToArray());

        foreach (var result in search.Data)
        {
            result.Parse(x =>
            {
                result.Id = x.Get(Searchable.FieldStrings[Searchable.Field.Id]);
                result.DescriptionPath = x.Get(Searchable.FieldStrings[Searchable.Field.DescriptionPath]);
                result.LinkText = x.Get(Searchable.FieldStrings[Searchable.Field.Title]);
                result.LinkHref = x.Get(Searchable.FieldStrings[Searchable.Field.Href]);
                result.Description = x.Get(Searchable.FieldStrings[Searchable.Field.Description]);
            });
        }

        var data = search.Data.GroupBy(s => s.Id.Split('_')[0]).Select(s => new
            SearchResponse
            {
                Text = s.Key,
                Children = s.ToList().Select(d => new SearchResponseChild() { Id = d.Id.Split('_')[1], Text = d.Description, Link = d.LinkHref })
                    .ToArray()
            });

        return Ok(new { results = data });
    }
}