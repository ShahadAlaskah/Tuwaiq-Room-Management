using System.Text.Json.Serialization;

namespace SDKIntegration.Models;

public class TokenResponse
{
    [JsonPropertyName("access_token")] public string AccessToken { get; set; }= null!;

    [JsonPropertyName("token_type")] public string TokenType { get; set; }= null!;

    [JsonPropertyName("expires_in")] public long ExpiresIn { get; set; }
}