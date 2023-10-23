using Refit;
using SDKIntegration.Models;

namespace SDKIntegration;

[Headers("Authorization: Bearer", "Accept: application/json")]
public interface ILookUpApi
{
    [Get("/Lookups/GetFormTemplateCategories")]
    Task<Refit.ApiResponse<PaginationResponse<LookupDictionary>>> GetFormTemplateCategories();
    
    [Get("/Lookups/GetFormTemplates/{formTemplateCategoryId}")]
    Task<Refit.ApiResponse<PaginationResponse<LookupDictionary>>> GetFormTemplates(Guid formTemplateCategoryId);
    
    [Get("/Lookups/GetFormTemplateCategoryByFormId/{formTemplateId}")]
    Task<Refit.ApiResponse<LookupDictionary>> GetFormTemplateCategoryByFormId(Guid formTemplateId);
}