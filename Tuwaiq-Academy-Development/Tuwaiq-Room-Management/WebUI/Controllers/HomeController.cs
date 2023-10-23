using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SDK;
using WebUI.Models;

namespace WebUI.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IRoomTypesApi _roomTypesApi;

    public HomeController(ILogger<HomeController> logger,IRoomTypesApi roomTypesApi)
    {
        _logger = logger;
        _roomTypesApi = roomTypesApi;
    }

    public async Task<IActionResult> Index()
    {
        var response = await _roomTypesApi.Get();
        return View();
    }

    // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    // public IActionResult Error()
    // {
    //     return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    // }
}