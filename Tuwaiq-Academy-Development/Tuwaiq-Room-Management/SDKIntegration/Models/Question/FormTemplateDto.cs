using SDKIntegration.Models.Enums;
using Tapper;

namespace SDKIntegration.Models.Question;

[TranspilationSource]
public class FormTemplateDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; } = null!;
    public bool IsActive { get; set; } = true;
    public PublishState PublishState { get; set; } = PublishState.Draft;
    public Guid FormTemplateCategoryId { get; set; }
    public FormTemplateCategoryDto? FormTemplateCategory { get; set; }
    public Guid? ExamAdvancedOptionId { get; set; }
    public ExamAdvancedOptionDto? ExamAdvancedOption { get; set; }

    public List<FormTemplateQuestionDto> FormTemplateQuestions { get; set; }

    public DateTime CreatedAt { get; set; }

    // protected override void AddCustomMappings()
    // {
    //     SetCustomMappingsInverse().AfterMapping((src, dest) =>
    //     {
    //         dest.FormTemplateQuestions = src.FormTemplateQuestions.Select(x => new FormTemplateQuestionDto()
    //         {
    //             
    //         }).ToList();
    //     });
    // }
}