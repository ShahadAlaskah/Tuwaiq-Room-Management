using Tapper;

namespace SDKIntegration.Models.Question;

[TranspilationSource] public class QuestionOptionNumberDto 
{
    public Guid Id { get;  set; }
    public string? Label { get; set; }
    public string? PlaceHolder { get; set; }
    public double? Min { get; set; }
    public double? Max { get; set; }
    public double? Step { get; set; }
    public double? DefaultValue { get; set; }
    public bool? AllowDecimal { get; set; }
    public bool? AllowNegative { get; set; }
}