using Tapper;

namespace SDK.Core;

[TranspilationSource]
public class QuestionTypes
{
    public bool Text { get; set; } = false;
    public bool LongText { get; set; } = false;
    public bool NationalId { get; set; } = false;
    public bool Email { get; set; } = false;
    public bool Number { get; set; } = false;
    public bool Radio { get; set; } = false;
    public bool Dropdown { get; set; } = false;
    public bool CheckBox { get; set; } = false;
    public bool Date { get; set; } = false;
    public bool File { get; set; } = false;
    public bool Slider { get; set; } = false;
}