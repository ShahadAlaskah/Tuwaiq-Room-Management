using SDKIntegration.Models.Enums;
using Tapper;

namespace SDKIntegration.Models.Question;

[TranspilationSource] public class QuestionOptionTextDto 
{
    public Guid Id { get;  set; }
    public string? Label { get; set; }
    public string? PlaceHolder { get; set; }
    public int? MinLength { get; set; }
    public int? MaxLength { get; set; }
    public string? DefaultValue { get; set; }
    public TextType TextType { get; set; }

}