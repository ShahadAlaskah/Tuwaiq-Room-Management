using System.Text.Json.Serialization;

namespace API.Models;

public class FileUploadResult
{
    [JsonPropertyName("size")]
    public long Size { set; get; }
    [JsonPropertyName("count")]
    public int Count { get; set; }
    [JsonPropertyName("fileNames")]
    public Dictionary<string, string> FileNames { get; set; } = null!;
}