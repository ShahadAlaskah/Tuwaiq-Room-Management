using SDKIntegration.Models.Enums;
using Tapper;

namespace SDKIntegration.Models.Question;

[TranspilationSource] public class QuestionOptionFileDto 
{
    public Guid Id { get;  set; }
    public string? Label { get; set; }
    public string? PlaceHolder { get; set; }
    public FileAcceptType FileAcceptType { get; set; }
    public decimal? MaxSizeInMb { get; set; }
    public int MaxFileCount { get; set; }

}