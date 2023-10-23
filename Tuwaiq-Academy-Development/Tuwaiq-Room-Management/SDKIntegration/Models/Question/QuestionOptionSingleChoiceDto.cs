using SDKIntegration.Models.Enums;
using Tapper;

namespace SDKIntegration.Models.Question;

[TranspilationSource] public class QuestionOptionSingleChoiceDto 
{
    public Guid Id { get;  set; }
    public SingleChoiceType SingleChoiceType { get; set; }

    public string? Label { get; set; }
    public HashSet<string> Options { get; set; }
    public string? DefaultValue { get; set; }

}