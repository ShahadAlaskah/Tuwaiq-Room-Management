using Tapper;

namespace SDKIntegration.Models;

[TranspilationSource]
public class LookupDictionary
{
    // ReSharper disable once InconsistentNaming
    public string id { get; set; } = null!;

    // ReSharper disable once InconsistentNaming
    public string text { get; set; } = null!;
}