using Tapper;

namespace SDKIntegration.Models.Question;

[TranspilationSource] public class QuestionOptionDateDto 
{
    public Guid Id { get;  set; }
    public string? Label { get; set; }
    public string? PlaceHolder { get; set; }
    public DateOnly? MinDate { get; set; }
    public DateOnly? MaxDate { get; set; }
    public TimeOnly? MinTime { get; set; }
    public TimeOnly? MaxTime { get; set; }

    public bool? MinToday { get; set; }
    public bool? MaxToday { get; set; }

    public DateTime? DefaultValue { get; set; }
    public bool AllowTime { get; set; }

}