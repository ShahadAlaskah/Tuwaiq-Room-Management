namespace WebUI.Models;

public class FormTemplateOptions
{
    public QuestionTypes Bootcamp { get; set; } = null!;
    public QuestionTypes Exams { get; set; } = null!;
    public QuestionTypes Others { get; set; } = null!;
}

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