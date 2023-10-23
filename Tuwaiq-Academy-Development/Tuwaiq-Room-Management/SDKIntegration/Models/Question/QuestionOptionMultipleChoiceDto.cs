using Tapper;

namespace SDKIntegration.Models.Question;

[TranspilationSource] public class QuestionOptionMultipleChoiceDto 
{
    public Guid Id { get;  set; }
    public string? Label { get; set; }
    public HashSet<string> Options { get; set; }
    public HashSet<string>? DefaultValues { get; set; }

}