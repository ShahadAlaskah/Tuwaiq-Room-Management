using Application.Search;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Validation.AspNetCore;
using Shared.Interfaces.ValidationErrors;

namespace API.Controllers;

[Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
[Route("[controller]")]
[ApiController]
public class AdminsController : ControllerBase
{
    private readonly ISender _sender;
    private readonly ISearchManager _searchManager;

    public AdminsController(ISender sender, ISearchManager searchManager)
    {
        _sender = sender;
        _searchManager = searchManager;
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [HttpGet("[action]")]
    public IActionResult ReInitializeSearch()
    {
        _searchManager.Clear();
        _searchManager.InitAll();

        return Ok();
    }
}