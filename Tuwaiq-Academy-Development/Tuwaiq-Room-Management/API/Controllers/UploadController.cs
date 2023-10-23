using API.Extensions;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Shared.Interfaces.ValidationErrors;

namespace API.Controllers;

[Authorize]
[Route("[controller]")]
[ApiController]
public class UploadController : ControllerBase
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IStringLocalizer<Shared.SharedResource> _sharedLocalization;

    public UploadController(IWebHostEnvironment webHostEnvironment,IStringLocalizer<Shared.SharedResource> sharedLocalization)
    {
        _webHostEnvironment = webHostEnvironment;
        _sharedLocalization = sharedLocalization;
    }

    [HttpPost]
    public async Task<IActionResult> Post(List<IFormFile> files)
    {
        long size = files.Sum(f => f.Length);
        Dictionary<string, string> fileNames = new Dictionary<string, string>();
        
        // Larger than 100MB
        if (size > 104857600)
        {
            return Error.Invalid(_sharedLocalization["FileSizeExceeded"]).Handle();
        }

        foreach (var formFile in files)
        {
            if (formFile.Length > 0)
            {
                var isImage = formFile.IsImage();
                string fileName = Path.GetRandomFileName() + Path.GetExtension(formFile.FileName);
                var filePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Storage", isImage ? "Images" : "Files", fileName);

                while (Path.Exists(filePath))
                {
                    fileName = Path.GetRandomFileName() + Path.GetExtension(formFile.FileName);
                    filePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Storage", isImage ? "Images" : "Files", fileName);
                }

                using (var stream = System.IO.File.Create(filePath))
                {
                    await formFile.CopyToAsync(stream);
                    fileNames.Add(formFile.FileName,  fileName);
                }
            }
        }

        // Process uploaded files
        // Don't rely on or trust the FileName property without validation.

        var response = new FileUploadResult
        {
            Count = files.Count,
            Size = size,
            FileNames = fileNames
        };
        return Ok(response);
    }
}