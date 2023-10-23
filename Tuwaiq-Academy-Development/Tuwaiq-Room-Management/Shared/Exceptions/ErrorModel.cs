using Newtonsoft.Json;

namespace Shared.Exceptions;

public class ErrorModel
{
    [JsonProperty("errors")] public Dictionary<string, string[]>? Errors { get; set; }
    [JsonProperty("type")] public string? Type { get; set; }
    [JsonProperty("title")] public string? Title { get; set; }
    [JsonProperty("status")] public int? Status { get; set; }
    [JsonProperty("traceId")] public string? TraceId { get; set; }
    
    public static ErrorModel? FromJson(string json) => JsonConvert.DeserializeObject<ErrorModel>(json);
}


public class SingleErrorModel
{
    [JsonProperty("errorCode")] public string? ErrorCode { get; set; }
    [JsonProperty("errorMessage")] public string? ErrorMessage { get; set; }
    [JsonProperty("traceId")] public string? TraceId { get; set; }
    public static SingleErrorModel? FromJson(string json) => JsonConvert.DeserializeObject<SingleErrorModel>(json);
}