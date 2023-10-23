using Tapper;

namespace SDKIntegration.Models.Question;

[TranspilationSource] public class QuestionOptionSliderDto 
{
    public Guid Id { get;  set; }
    public string? Label { get; set; }

    public Dictionary<decimal, string> Steps { get; set; }
    public decimal? DefaultValue { get; set; }

}