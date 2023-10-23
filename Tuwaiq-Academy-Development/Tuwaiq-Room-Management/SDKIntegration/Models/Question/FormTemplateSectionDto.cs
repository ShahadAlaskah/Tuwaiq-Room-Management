using Tapper;

namespace SDKIntegration.Models.Question;

[TranspilationSource] public class FormTemplateSectionDto 
{
    public Guid Id { get; set; } 
    public string Title { get; set; }
    public string? Description { get; set; }
    public int Order { get; set; }
}