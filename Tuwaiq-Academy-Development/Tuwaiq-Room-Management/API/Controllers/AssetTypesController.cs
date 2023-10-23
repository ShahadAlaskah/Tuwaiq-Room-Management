using API.Helpers;
using Application.Commands.AssetTypes.Commands;
using Application.Dto;
using Application.Persistence.Queries;
using Application.Specifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Validation.AspNetCore;
using Shared.Ids;
using Shared.Interfaces.ValidationErrors;

namespace API.Controllers;

[Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
[Route("[controller]")]
[ApiController]
public class AssetTypesController : ControllerBase
{
    private readonly ISender _sender;

    public AssetTypesController(ISender sender)
    {
        _sender = sender;
    }

    [ProducesResponseType(typeof(ApiResponse<AssetTypeDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [HttpGet("[action]")]
    [HttpGet("[action]/{pageSize}/{page}")]
    [HttpGet("[action]/{pageSize}/{page}/{query}")]
    public async Task<IActionResult> Get(int? page = 1, int? pageSize = 10, string? query = null)
    {
        if (pageSize > 20) pageSize = 20;
        var result = await _sender.Send(new GetAssetTypeQuery(new GetAssetTypeSpecification(query), page ?? 1, pageSize ?? 10));
        if (result.Failure) return BadRequest(result.Error);

        Response.Headers.Add("firstPage",
            result.Value.HasPreviousPage ? $"{Url.Action("Get", new { page = 1, pageSize = pageSize, query = query })}" : "");
        Response.Headers.Add("prevPage",
            result.Value.HasPreviousPage ? $"{Url.Action("Get", new { page = page - 1, pageSize = pageSize, query = query })}" : "");
        Response.Headers.Add("nextPage",
            result.Value.HasNextPage ? $"{Url.Action("Get", new { page = page + 1, pageSize = pageSize, query = query })}" : "");
        Response.Headers.Add("lastPage", result.Value.HasNextPage
            ? $"{Url.Action("Get", new { page = result.Value.TotalPages, pageSize = pageSize, query = query })}"
            : "");

        return Ok(new ApiResponse<AssetTypeDto>
        {
            Pagination = new()
            {
                Offset = (page ?? 1) + (pageSize ?? 10),
                Limit = pageSize ?? 10,
                Total = result.Value.TotalCount,
                TotalPages = result.Value.TotalPages
            },
            Data = result.Value.ToList(),
        });
    }

    [ProducesResponseType(typeof(AssetTypeDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [HttpGet("[action]/{id}")]
    public async Task<IActionResult> Get(AssetTypeId id)
    {
        var result = await _sender.Send(new GetAssetTypeQuery(new GetAssetTypeSpecification(id), 1, 1));
        if (result.Failure) return BadRequest(result.Error);
        return Ok(result.Value.FirstOrDefault());
    }

    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [HttpPost("[action]")]
    public async Task<IActionResult> Post(CreateAssetTypeCommand command)
    {
        var result = await _sender.Send(command);
        if (result.Failure) return BadRequest(result.Error);

        var toReturn = await _sender.Send(new GetAssetTypeQuery(new GetAssetTypeSpecification(result.Value), 1, 1));
        if (toReturn.Failure) return BadRequest(toReturn.Error);
        return CreatedAtAction(nameof(Get), new { id = result.Value.Value.ToString() }, toReturn.Value.FirstOrDefault());
    }

    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [HttpPut("[action]")]
    public async Task<IActionResult> Put(UpdateAssetTypeCommand command)
    {
        var result = await _sender.Send(command);
        if (result.Failure) return BadRequest(result.Error);
        return Accepted();
    }

    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [HttpDelete("[action]/{id}")]
    public async Task<IActionResult> Delete(AssetTypeId id)
    {
        var result = await _sender.Send(new DeleteAssetTypeCommand(id));
        if (result.Failure) return BadRequest(result.Error);
        return NoContent();
    }
}