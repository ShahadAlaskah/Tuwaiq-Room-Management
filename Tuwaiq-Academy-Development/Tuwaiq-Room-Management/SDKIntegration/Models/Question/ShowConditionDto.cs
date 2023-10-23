using Tapper;

namespace SDKIntegration.Models.Question;


[TranspilationSource] public class ShowConditionDto 
{
    public Guid FormTemplateQuestionId { get; set; }
    public string Value { get; set; } = null!;
}