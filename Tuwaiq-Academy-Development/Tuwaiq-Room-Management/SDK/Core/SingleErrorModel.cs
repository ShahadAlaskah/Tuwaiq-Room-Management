using Newtonsoft.Json;
using Tapper;

namespace SDK.Core;

[TranspilationSource] public class SingleErrorModel
{
    [JsonProperty("errorCode")] public string? ErrorCode { get; set; }
    [JsonProperty("errorMessage")] public string? ErrorMessage { get; set; }
    [JsonProperty("traceId")] public string? TraceId { get; set; }
    public static SingleErrorModel? FromJson(string json) => JsonConvert.DeserializeObject<SingleErrorModel>(json);
}