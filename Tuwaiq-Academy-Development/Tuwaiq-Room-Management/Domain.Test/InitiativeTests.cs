// using Domain.Domains;
// using Domain.Enums;
// using FluentAssertions;
// using Shared.Ids;
//
// namespace Domain.Test;
//
// public class FormTemplatesTests
// {
//     private FormTemplate _formTemplate;
//     private FormTemplateCategory _formTemplateCategory;
//
//     [SetUp]
//     public void Setup()
//     {
//         _formTemplate = new FormTemplate();
//         _formTemplateCategory = new FormTemplateCategory("Cat1");
//     }
//
//     [Test, Order(1)]
//     public void ValidateEmptyProperties()
//     {
//         _formTemplate.Title.Should().BeNullOrEmpty();
//         _formTemplate.Description.Should().BeNullOrEmpty();
//         _formTemplate.IsActive.Should().BeTrue();
//         _formTemplate.PublishState.Should().Be(PublishState.Draft);
//         _formTemplate.FormTemplateCategoryId.Should().Be(FormTemplateCategoryId.Empty);
//         _formTemplate.FormTemplateQuestions.Count.Should().Be(0);
//     }
//
//     [Test, Order(2)]
//     public void ValidateQuestionNumber()
//     {
//         _formTemplate.Title = "Title";
//         _formTemplate.Description = "Description";
//         _formTemplate.IsActive = false;
//         _formTemplate.FormTemplateCategoryId = _formTemplateCategory.Id;
//         var result = _formTemplate.AddNumberQuestion("Title", 1, "Label", "PlaceHolder", 0, 0, 0,
//             null, false, false);
//
//         result.Success.Should().BeTrue();
//
//         _formTemplate.Title.Should().Be("Title");
//         _formTemplate.Description.Should().Be("Description");
//         _formTemplate.IsActive.Should().BeFalse();
//         _formTemplate.PublishState.Should().Be(PublishState.Draft);
//         _formTemplate.FormTemplateCategoryId.Should().Be(_formTemplateCategory.Id);
//         _formTemplate.FormTemplateQuestions.Count.Should().Be(1);
//
//         _formTemplate.FormTemplateQuestions.First().Title.Should().Be("Title");
//         _formTemplate.FormTemplateQuestions.First().Order.Should().Be(1);
//         _formTemplate.FormTemplateQuestions.First().QuestionType.Should().Be(QuestionType.Number);
//         _formTemplate.FormTemplateQuestions.First().IsRequired.Should().BeTrue();
//         _formTemplate.FormTemplateQuestions.First().QuestionOptionNumber.Label.Should().Be("Label");
//         _formTemplate.FormTemplateQuestions.First().QuestionOptionNumber.PlaceHolder.Should().Be("PlaceHolder");
//         _formTemplate.FormTemplateQuestions.First().QuestionOptionNumber.Min.Should().Be(0);
//         _formTemplate.FormTemplateQuestions.First().QuestionOptionNumber.Max.Should().Be(0);
//         _formTemplate.FormTemplateQuestions.First().QuestionOptionNumber.Step.Should().Be(0);
//         _formTemplate.FormTemplateQuestions.First().QuestionOptionNumber.DefaultValue.Should().Be(null);
//         _formTemplate.FormTemplateQuestions.First().QuestionOptionNumber.AllowDecimal.Should().BeFalse();
//         _formTemplate.FormTemplateQuestions.First().QuestionOptionNumber.AllowNegative.Should().BeFalse();
//     }
//
//     [Test, Order(3)]
//     public void ValidateQuestionSlider()
//     {
//         _formTemplate.Title = "Title";
//         _formTemplate.Description = "Description";
//         _formTemplate.IsActive = false;
//         _formTemplate.FormTemplateCategoryId = _formTemplateCategory.Id;
//         var result = _formTemplate.AddSliderQuestion("Title", 1, "Label",
//             new Dictionary<decimal, string>() { { 1.0m, "a" } }, 1.0m);
//
//         result.Success.Should().BeFalse();
//         result.Error?.ErrorMessage.Should().Be("Steps");
//         _formTemplate.FormTemplateQuestions.Count.Should().Be(0);
//
//
//         result = _formTemplate.AddSliderQuestion("Title", 1, "Label",
//             new Dictionary<decimal, string>() { { 1.0m, "a" }, { 2.0m, "b" } }, 1.0m);
//
//         result.Success.Should().BeTrue();
//
//         _formTemplate.Title.Should().Be("Title");
//         _formTemplate.Description.Should().Be("Description");
//         _formTemplate.IsActive.Should().BeFalse();
//         _formTemplate.PublishState.Should().Be(PublishState.Draft);
//         _formTemplate.FormTemplateCategoryId.Should().Be(_formTemplateCategory.Id);
//         _formTemplate.FormTemplateQuestions.Count.Should().Be(1);
//
//         _formTemplate.FormTemplateQuestions.Last().Title.Should().Be("Title");
//         _formTemplate.FormTemplateQuestions.Last().Order.Should().Be(1);
//         _formTemplate.FormTemplateQuestions.Last().QuestionType.Should().Be(QuestionType.Slider);
//         _formTemplate.FormTemplateQuestions.Last().IsRequired.Should().BeTrue();
//         _formTemplate.FormTemplateQuestions.Last().QuestionOptionSlider.Label.Should().Be("Label");
//         _formTemplate.FormTemplateQuestions.Last().QuestionOptionSlider.Steps.Should().NotBeEmpty();
//         _formTemplate.FormTemplateQuestions.Last().QuestionOptionSlider.Steps.Count.Should().Be(2);
//         _formTemplate.FormTemplateQuestions.Last().QuestionOptionSlider.DefaultValue.Should().Be(1.0m);
//     }
//
//     [Test, Order(4)]
//     public void ValidateQuestionText()
//     {
//         _formTemplate.Title = "Title";
//         _formTemplate.Description = "Description";
//         _formTemplate.IsActive = false;
//         _formTemplate.FormTemplateCategoryId = _formTemplateCategory.Id;
//         var result = _formTemplate.AddTextQuestion("Title", 1, "", "", "Label", "PlaceHolder", TextType.Text, 0, 0, "", false);
//
//         result.Success.Should().BeFalse();
//         result.Error?.ErrorCode.Should().Be("Invalid");
//         result.Error?.ErrorMessage.Should().Be("MinLength");
//
//         result = _formTemplate.AddTextQuestion("Title", 1, "", "","Label", "PlaceHolder", TextType.Text, 2, 1, "", false);
//
//         result.Success.Should().BeFalse();
//         result.Error?.ErrorCode.Should().Be("Invalid");
//         result.Error?.ErrorMessage.Should().Be("MinLength");
//
//         result = _formTemplate.AddTextQuestion("Title", 1, "", "","Label", "PlaceHolder", TextType.Text, 1, 3, null, false);
//         result.Success.Should().BeTrue();
//
//         _formTemplate.Title.Should().Be("Title");
//         _formTemplate.Description.Should().Be("Description");
//         _formTemplate.IsActive.Should().BeFalse();
//         _formTemplate.PublishState.Should().Be(PublishState.Draft);
//         _formTemplate.FormTemplateCategoryId.Should().Be(_formTemplateCategory.Id);
//         _formTemplate.FormTemplateQuestions.Count.Should().Be(1);
//
//         _formTemplate.FormTemplateQuestions.First().Title.Should().Be("Title");
//         _formTemplate.FormTemplateQuestions.First().Order.Should().Be(1);
//         _formTemplate.FormTemplateQuestions.First().QuestionType.Should().Be(QuestionType.Text);
//         _formTemplate.FormTemplateQuestions.First().IsRequired.Should().BeFalse();
//         _formTemplate.FormTemplateQuestions.First().QuestionOptionText.Label.Should().Be("Label");
//         _formTemplate.FormTemplateQuestions.First().QuestionOptionText.PlaceHolder.Should().Be("PlaceHolder");
//         _formTemplate.FormTemplateQuestions.First().QuestionOptionText.MinLength.Should().Be(1);
//         _formTemplate.FormTemplateQuestions.First().QuestionOptionText.TextType.Should().Be(TextType.Text);
//         _formTemplate.FormTemplateQuestions.First().QuestionOptionText.MaxLength.Should().Be(3);
//         _formTemplate.FormTemplateQuestions.First().QuestionOptionText.DefaultValue.Should().Be(null);
//     }
//
//     [Test, Order(5)]
//     public void ValidateQuestionTextEmail()
//     {
//         _formTemplate.Title = "Title";
//         _formTemplate.Description = "Description";
//         _formTemplate.IsActive = false;
//         _formTemplate.FormTemplateCategoryId = _formTemplateCategory.Id;
//         var result = _formTemplate.AddTextQuestion("Title", 1, "", "", "Label", "PlaceHolder", TextType.Email, 0, 0, "", false);
//
//         result.Success.Should().BeFalse();
//         result.Error?.ErrorCode.Should().Be("Invalid");
//         result.Error?.ErrorMessage.Should().Be("MinLength");
//
//         result = _formTemplate.AddTextQuestion("Title", 1, "", "", "Label", "PlaceHolder", TextType.Email, 2, 1, "", false);
//
//         result.Success.Should().BeFalse();
//         result.Error?.ErrorCode.Should().Be("Invalid");
//         result.Error?.ErrorMessage.Should().Be("MinLength");
//
//         result = _formTemplate.AddTextQuestion("Title", 1, "", "", "Label", "PlaceHolder", TextType.Email, 1, 3, null, false);
//         result.Success.Should().BeTrue();
//
//         _formTemplate.Title.Should().Be("Title");
//         _formTemplate.Description.Should().Be("Description");
//         _formTemplate.IsActive.Should().BeFalse();
//         _formTemplate.PublishState.Should().Be(PublishState.Draft);
//         _formTemplate.FormTemplateCategoryId.Should().Be(_formTemplateCategory.Id);
//         _formTemplate.FormTemplateQuestions.Count.Should().Be(1);
//
//         _formTemplate.FormTemplateQuestions.First().Title.Should().Be("Title");
//         _formTemplate.FormTemplateQuestions.First().Order.Should().Be(1);
//         _formTemplate.FormTemplateQuestions.First().QuestionType.Should().Be(QuestionType.Text);
//         _formTemplate.FormTemplateQuestions.First().IsRequired.Should().BeFalse();
//         _formTemplate.FormTemplateQuestions.First().QuestionOptionText.Label.Should().Be("Label");
//         _formTemplate.FormTemplateQuestions.First().QuestionOptionText.PlaceHolder.Should().Be("PlaceHolder");
//         _formTemplate.FormTemplateQuestions.First().QuestionOptionText.MinLength.Should().Be(1);
//         _formTemplate.FormTemplateQuestions.First().QuestionOptionText.TextType.Should().Be(TextType.Email);
//         _formTemplate.FormTemplateQuestions.First().QuestionOptionText.MaxLength.Should().Be(3);
//         _formTemplate.FormTemplateQuestions.First().QuestionOptionText.DefaultValue.Should().Be(null);
//     }
//
//     [Test, Order(6)]
//     public void ValidateQuestionTextNationalId()
//     {
//         _formTemplate.Title = "Title";
//         _formTemplate.Description = "Description";
//         _formTemplate.IsActive = false;
//         _formTemplate.FormTemplateCategoryId = _formTemplateCategory.Id;
//         var result = _formTemplate.AddTextQuestion("Title", 1, "", "", "Label", "PlaceHolder", TextType.NationalId, 0, 0, "", false);
//
//         result.Success.Should().BeFalse();
//         result.Error?.ErrorCode.Should().Be("Invalid");
//         result.Error?.ErrorMessage.Should().Be("MinLength");
//
//         result = _formTemplate.AddTextQuestion("Title", 1, "", "", "Label", "PlaceHolder", TextType.NationalId, 2, 1, "", false);
//
//         result.Success.Should().BeFalse();
//         result.Error?.ErrorCode.Should().Be("Invalid");
//         result.Error?.ErrorMessage.Should().Be("MinLength");
//
//         result = _formTemplate.AddTextQuestion("Title", 1, "", "", "Label", "PlaceHolder", TextType.NationalId, 1, 3, null, false);
//         result.Success.Should().BeTrue();
//
//         _formTemplate.Title.Should().Be("Title");
//         _formTemplate.Description.Should().Be("Description");
//         _formTemplate.IsActive.Should().BeFalse();
//         _formTemplate.PublishState.Should().Be(PublishState.Draft);
//         _formTemplate.FormTemplateCategoryId.Should().Be(_formTemplateCategory.Id);
//         _formTemplate.FormTemplateQuestions.Count.Should().Be(1);
//
//         _formTemplate.FormTemplateQuestions.First().Title.Should().Be("Title");
//         _formTemplate.FormTemplateQuestions.First().Order.Should().Be(1);
//         _formTemplate.FormTemplateQuestions.First().QuestionType.Should().Be(QuestionType.Text);
//         _formTemplate.FormTemplateQuestions.First().IsRequired.Should().BeFalse();
//         _formTemplate.FormTemplateQuestions.First().QuestionOptionText.Label.Should().Be("Label");
//         _formTemplate.FormTemplateQuestions.First().QuestionOptionText.PlaceHolder.Should().Be("PlaceHolder");
//         _formTemplate.FormTemplateQuestions.First().QuestionOptionText.MinLength.Should().Be(1);
//         _formTemplate.FormTemplateQuestions.First().QuestionOptionText.TextType.Should().Be(TextType.NationalId);
//         _formTemplate.FormTemplateQuestions.First().QuestionOptionText.MaxLength.Should().Be(3);
//         _formTemplate.FormTemplateQuestions.First().QuestionOptionText.DefaultValue.Should().Be(null);
//     }
//
//     [Test, Order(7)]
//     public void ValidateQuestionDate()
//     {
//         _formTemplate.Title = "Title";
//         _formTemplate.Description = "Description";
//         _formTemplate.IsActive = false;
//         _formTemplate.FormTemplateCategoryId = _formTemplateCategory.Id;
//         var result = _formTemplate.AddDateQuestion("Title", 1, "Label", "PlaceHolder", false, false, false,
//             new DateOnly(2023, 1, 1), new DateOnly(2023, 1, 1));
//
//         result.Success.Should().BeFalse();
//         result.Error?.ErrorCode.Should().Be("Invalid");
//         result.Error?.ErrorMessage.Should().Be("MinDate");
//
//         result = _formTemplate.AddDateQuestion("Title", 1, "Label", "PlaceHolder", false, false, false,
//             new DateOnly(2023, 1, 1), new DateOnly(2023, 1, 2), new TimeOnly(1, 1, 1), new TimeOnly(1, 1, 1));
//
//         result.Success.Should().BeFalse();
//         result.Error?.ErrorCode.Should().Be("Invalid");
//         result.Error?.ErrorMessage.Should().Be("MinTime");
//
//         result = _formTemplate.AddDateQuestion("Title", 1, "Label", "PlaceHolder", false, false, false, new DateOnly(2023, 1, 1),
//             new DateOnly(2023, 1, 2), null, null, null, false);
//         result.Success.Should().BeTrue();
//
//         _formTemplate.Title.Should().Be("Title");
//         _formTemplate.Description.Should().Be("Description");
//         _formTemplate.IsActive.Should().BeFalse();
//         _formTemplate.PublishState.Should().Be(PublishState.Draft);
//         _formTemplate.FormTemplateCategoryId.Should().Be(_formTemplateCategory.Id);
//         _formTemplate.FormTemplateQuestions.Count.Should().Be(1);
//
//         _formTemplate.FormTemplateQuestions.First().Title.Should().Be("Title");
//         _formTemplate.FormTemplateQuestions.First().Order.Should().Be(1);
//         _formTemplate.FormTemplateQuestions.First().QuestionType.Should().Be(QuestionType.Date);
//         _formTemplate.FormTemplateQuestions.First().IsRequired.Should().BeFalse();
//         _formTemplate.FormTemplateQuestions.First().QuestionOptionDate.Label.Should().Be("Label");
//         _formTemplate.FormTemplateQuestions.First().QuestionOptionDate.PlaceHolder.Should().Be("PlaceHolder");
//         _formTemplate.FormTemplateQuestions.First().QuestionOptionDate.DefaultValue.Should().Be(null);
//         _formTemplate.FormTemplateQuestions.First().QuestionOptionDate.MinDate.Should().Be(new DateOnly(2023, 1, 1));
//         _formTemplate.FormTemplateQuestions.First().QuestionOptionDate.MaxDate.Should().Be(new DateOnly(2023, 1, 2));
//     }
//
//     [Test, Order(8)]
//     public void ValidateQuestionSingleChoice()
//     {
//         _formTemplate.Title = "Title";
//         _formTemplate.Description = "Description";
//         _formTemplate.IsActive = false;
//         _formTemplate.FormTemplateCategoryId = _formTemplateCategory.Id;
//         var result = _formTemplate.AddSingleChoiceQuestion("Title", 1, "Label", new HashSet<string>() { "option 1" });
//
//         result.Success.Should().BeFalse();
//         result.Error?.ErrorCode.Should().Be("Invalid");
//         result.Error?.ErrorMessage.Should().Be("Options");
//
//         result = _formTemplate.AddSingleChoiceQuestion("Title", 1, "Label", new HashSet<string>() { "option 1", "option 2" },
//             "option 4", SingleChoiceType.Dropdown);
//
//         result.Success.Should().BeFalse();
//         result.Error?.ErrorCode.Should().Be("Invalid");
//         result.Error?.ErrorMessage.Should().Be("DefaultValue");
//
//         result = _formTemplate.AddSingleChoiceQuestion("Title", 1, "Label", new HashSet<string>() { "option 1", "option 2" },
//             "option 1", SingleChoiceType.Dropdown);
//         result.Success.Should().BeTrue();
//
//         _formTemplate.Title.Should().Be("Title");
//         _formTemplate.Description.Should().Be("Description");
//         _formTemplate.IsActive.Should().BeFalse();
//         _formTemplate.PublishState.Should().Be(PublishState.Draft);
//         _formTemplate.FormTemplateCategoryId.Should().Be(_formTemplateCategory.Id);
//         _formTemplate.FormTemplateQuestions.Count.Should().Be(1);
//
//         _formTemplate.FormTemplateQuestions.First().Title.Should().Be("Title");
//         _formTemplate.FormTemplateQuestions.First().Order.Should().Be(1);
//         _formTemplate.FormTemplateQuestions.First().QuestionType.Should().Be(QuestionType.Radio);
//         _formTemplate.FormTemplateQuestions.First().IsRequired.Should().BeTrue();
//         _formTemplate.FormTemplateQuestions.First().QuestionOptionSingleChoice.Label.Should().Be("Label");
//         _formTemplate.FormTemplateQuestions.First().QuestionOptionSingleChoice.DefaultValue.Should().Be("option 1");
//         _formTemplate.FormTemplateQuestions.First().QuestionOptionSingleChoice.Options.Should()
//             .BeEquivalentTo(new HashSet<string>() { "option 1", "option 2" });
//         _formTemplate.FormTemplateQuestions.First().QuestionOptionSingleChoice.SingleChoiceType.Should()
//             .Be(SingleChoiceType.Dropdown);
//     }
//
//
//     [Test, Order(9)]
//     public void ValidateQuestionMultipleChoice()
//     {
//         _formTemplate.Title = "Title";
//         _formTemplate.Description = "Description";
//         _formTemplate.IsActive = false;
//         _formTemplate.FormTemplateCategoryId = _formTemplateCategory.Id;
//         var result = _formTemplate.AddMultipleChoicesQuestion("Title", 1, "Label", new HashSet<string>() { "option 1" });
//
//         result.Success.Should().BeFalse();
//         result.Error?.ErrorCode.Should().Be("Invalid");
//         result.Error?.ErrorMessage.Should().Be("Options");
//
//         result = _formTemplate.AddMultipleChoicesQuestion("Title", 1, "Label", new HashSet<string>() { "option 1", "option 2" },
//             new HashSet<string>() { "option 4" });
//
//         result.Success.Should().BeFalse();
//         result.Error?.ErrorCode.Should().Be("Invalid");
//         result.Error?.ErrorMessage.Should().Be("DefaultValues");
//
//         result = _formTemplate.AddMultipleChoicesQuestion("Title", 1, "Label", new HashSet<string>() { "option 1", "option 2" },
//             new HashSet<string>() { "option 1" });
//         result.Success.Should().BeTrue();
//
//         _formTemplate.Title.Should().Be("Title");
//         _formTemplate.Description.Should().Be("Description");
//         _formTemplate.IsActive.Should().BeFalse();
//         _formTemplate.PublishState.Should().Be(PublishState.Draft);
//         _formTemplate.FormTemplateCategoryId.Should().Be(_formTemplateCategory.Id);
//         _formTemplate.FormTemplateQuestions.Count.Should().Be(1);
//
//         _formTemplate.FormTemplateQuestions.First().Title.Should().Be("Title");
//         _formTemplate.FormTemplateQuestions.First().Order.Should().Be(1);
//         _formTemplate.FormTemplateQuestions.First().QuestionType.Should().Be(QuestionType.CheckBox);
//         _formTemplate.FormTemplateQuestions.First().IsRequired.Should().BeTrue();
//         _formTemplate.FormTemplateQuestions.First().QuestionOptionMultipleChoice.Label.Should().Be("Label");
//         _formTemplate.FormTemplateQuestions.First().QuestionOptionMultipleChoice.DefaultValues.Should()
//             .BeEquivalentTo(new HashSet<string>() { "option 1" });
//         _formTemplate.FormTemplateQuestions.First().QuestionOptionMultipleChoice.Options.Should()
//             .BeEquivalentTo(new HashSet<string>() { "option 1", "option 2" });
//     }
// }