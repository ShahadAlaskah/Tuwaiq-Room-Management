using SDKIntegration.Models.Enums;
using Tapper;

namespace SDKIntegration.Models.Question;

[TranspilationSource] public class FormTemplateCategoryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public CategoryType CategoryType { get; set; }
    public bool IsActive { get; set; }
}