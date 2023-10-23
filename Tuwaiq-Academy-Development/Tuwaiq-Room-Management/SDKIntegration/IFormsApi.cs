using Refit;
using SDKIntegration.Models.Question;

namespace SDKIntegration;

[Headers("Authorization: Bearer", "Accept: application/json")]
public interface IFormsApi
{
    [Get("/FormTemplates/Get/{id}")]
    Task<Refit.ApiResponse<FormTemplateDto>> GetFormTemplate(Guid id);
    
}