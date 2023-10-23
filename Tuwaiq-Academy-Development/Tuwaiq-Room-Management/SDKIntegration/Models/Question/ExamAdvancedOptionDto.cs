using System.ComponentModel.DataAnnotations;
using Tapper;

namespace SDKIntegration.Models.Question;

[TranspilationSource]
public class ExamAdvancedOptionDto
{
    public Guid Id { get; set; }
    [Display(Name = "Duration")] public TimeOnly? Duration { get; set; }

    [Display(Name = "RequireAuthentication")]
    public bool RequireAuthentication { get; set; }

    [Display(Name = "QuestionRandomize")] public bool QuestionRandomize { get; set; }
    [Display(Name = "QuestionPerPage")] public int QuestionPerPage { get; set; }
    [Display(Name = "TimePerPage")] public TimeOnly? TimePerPage { get; set; }
    [Display(Name = "CanGoBack")] public bool CanGoBack { get; set; }
}