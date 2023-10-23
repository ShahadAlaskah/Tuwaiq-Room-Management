using Tapper;

namespace SDK.Core;

[TranspilationSource] public class DashboardResponse
{
    public int FormTemplateCategoryCount { get; set; }
    public int FormTemplateCategoryBootcampFormsCount { get; set; }
    public int FormTemplateCategoryExamsFormsCount { get; set; }
    public int FormTemplateCategoryOthersCount { get; set; }
    
    public int FormTemplateCount { get; set; }
    public int FormTemplateBootcampFormsCount { get; set; }
    public int FormTemplateExamsFormsCount { get; set; }
    public int FormTemplateOthersCount { get; set; }
    
    public int FormTemplateBootcampFormsActiveCount { get; set; }
    public int FormTemplateExamsFormsActiveCount { get; set; }
    public int FormTemplateOthersActiveCount { get; set; }
}