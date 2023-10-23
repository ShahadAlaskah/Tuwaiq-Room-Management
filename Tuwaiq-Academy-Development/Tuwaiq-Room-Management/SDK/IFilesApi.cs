using Microsoft.AspNetCore.Http;
using Refit;

namespace SDK;

[Headers("Authorization: Bearer", "Accept: application/json")]
public interface IFilesApi
{
    // [Multipart]
    // [Post("/Upload")] 
    // Task<FileUploadResult> Upload(List<StreamPart> files);

    [Headers("Accept: application/json")]
    [Get("/Download")]
    Task<FormFile> Download(string fileName);

    [Headers("Accept: application/json")]
    [Get("/images/{filename}")]
    Task<HttpContent> Images(string filename, string? w = null, string? h = null, string? mode = null);

}
