using SDKIntegration.Models.Enums;
using Tapper;

namespace SDKIntegration.Models.Question;

[TranspilationSource] public class FormTemplateQuestionDto 
{
    public Guid Id { get;  set; }

    public Guid FormTemplateId { get; set; }
    public FormTemplateDto? FormTemplate { get; set; }

    public string Title { get; set; } = null!;
    public string? Description { get; set; } = null!;
    public int Order { get; set; }
    public string? FieldName { get; set; } = null!;
    public bool IsRequired { get; set; }
    public string CorrectAnswer { get; set; } = null!;

    public ShowConditionDto? ShowCondition { get; set; }

    public QuestionType QuestionType { get; set; }

    public Guid? FormTemplateSectionId { get; set; }
    public FormTemplateSectionDto? FormTemplateSection { get; set; }

    public QuestionOptionNumberDto QuestionOptionNumber { get;  set; } = null!;
    public QuestionOptionSingleChoiceDto QuestionOptionSingleChoice { get;  set; } = null!;
    public QuestionOptionMultipleChoiceDto QuestionOptionMultipleChoice { get;  set; } = null!;
    public QuestionOptionTextDto QuestionOptionText { get;  set; } = null!;
    public QuestionOptionDateDto QuestionOptionDate { get;  set; } = null!;
    public QuestionOptionFileDto QuestionOptionFile { get;  set; } = null!;
    public QuestionOptionSliderDto QuestionOptionSlider { get;  set; } = null!;
    
}