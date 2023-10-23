using Tapper;

namespace SDK.Core;

[TranspilationSource]
public class FormTemplateOptions
{
    public QuestionTypes Bootcamp { get; set; } = null!;
    public QuestionTypes Exams { get; set; } = null!;
    public QuestionTypes Others { get; set; } = null!;
}