using API.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Validation.AspNetCore;

namespace API.Controllers;

[Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
[Route("[controller]")]
[ApiController]
public class DownloadController : ControllerBase
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public DownloadController(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    [HttpGet]
    public async Task<IActionResult> Get(string fileName)
    {
        
        if (string.IsNullOrEmpty(fileName.Trim()))
        {
            return BadRequest("InvalidFileName");
        }
        
        var isImage = fileName.IsImage();

        var path  = Path.Combine(_webHostEnvironment.ContentRootPath, "Storage", isImage ? "Images" : "Files", fileName);


        if (Path.Exists(path))
        {
            // var net = new System.Net.WebClient();
            // var data = net.DownloadData(path);
            // var content = new System.IO.MemoryStream(data);
            // var contentType = "application/force-download";
            // return File(content, contentType, fileName);
            
            var bytes = await System.IO.File.ReadAllBytesAsync(path);
            return File(bytes, "application/force-download", fileName);

        }


        return BadRequest("NotFound");
    }
}