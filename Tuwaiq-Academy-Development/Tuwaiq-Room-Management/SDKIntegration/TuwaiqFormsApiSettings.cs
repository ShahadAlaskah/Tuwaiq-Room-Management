using System.ComponentModel.DataAnnotations;

namespace SDKIntegration;

public class TuwaiqFormsApiSettings
{
    [Required] public string Url { get; set; } = null!;
    [Required] public string IdentityServerBaseUrl { get; set; } = null!;
    [Required] public string IdentityServerTokenUrl { get; set; } = null!;
    [Required] public string ClientId { get; set; } = null!;
    [Required] public string ClientSecret { get; set; } = null!;
    [Required] public string[] Scopes { get; set; } = null!;
}