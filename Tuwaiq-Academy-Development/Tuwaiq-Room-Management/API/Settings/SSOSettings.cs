namespace API.Settings;

// ReSharper disable once InconsistentNaming
public class SSOSettings
{
    public string IdentityServerUrl { get; set; } = null!;
    public string AddAudience { get; set; }= null!;
    public string ClientId { get; set; }= null!;
    public string ClientSecret { get; set; }= null!;
}