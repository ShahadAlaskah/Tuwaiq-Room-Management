using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Refit;
using SDK;

namespace WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IFilesApi _filesApi;
        private readonly TuwaiqRoomManagementApiSettings _settings;

        public FilesController(IFilesApi filesApi,IOptions<TuwaiqRoomManagementApiSettings> settings)
        {
            _filesApi = filesApi;
            _settings = settings.Value;
        }

        [ValidateAntiForgeryToken]
        [HttpGet("[action]")]
        public async Task<IActionResult> Download(string filename)
        {
            if (!string.IsNullOrEmpty(filename))
            {
                var result = await _filesApi.Download(filename);
                return Ok(result);
            }

            return Ok();
        }

        // [ValidateAntiForgeryToken]
        [HttpGet("[action]/{filename}")]
        public async Task<IActionResult> Images(string filename, string? w = null, string? h = null, string? mode = null)
        {
            if (!string.IsNullOrEmpty(filename))
            {
                var result = await _filesApi.Images(filename, w, h, mode);
                byte[] bytes = await result.ReadAsByteArrayAsync();
                return File(bytes, "image/jpeg");
            }

            return Ok();
        }
        
        //Create Action to Upload File and Push it to refit api
        // [ValidateAntiForgeryToken]
        // [HttpPost("[action]")]
        // public async Task<IActionResult> Upload(List<IFormFile> files)
        // {
        //     if (files.Count > 0)
        //     {
        //         //convert IFormFile to Stream
        //         var streamFiles = new List<StreamPart>();
        //         foreach (var file in files)
        //         {
        //             var stream = file.OpenReadStream();
        //             streamFiles.Add(new StreamPart(stream, file.FileName, file.ContentType));
        //         }
        //         var result = await _filesApi.Upload(streamFiles);
        //         return Ok(result);
        //     }
        //
        //     return BadRequest();
        // }
    }
}