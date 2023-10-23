using Microsoft.AspNetCore.Mvc.ModelBinding;
using Refit;
using ErrorModel = SDK.Core.ErrorModel;
using SingleErrorModel = Shared.Exceptions.SingleErrorModel;

namespace WebUI.Extensions;

public static class ApiResponseExtensions
{
    public static void HandelError(this IApiResponse response, ModelStateDictionary modelState)
    {
        if (response.Error?.Content != null)
        {
            var error = ErrorModel.FromJson(response.Error.Content);
            if (error?.Errors != null)
            {
                foreach (var (key, value) in error.Errors)
                {
                    modelState.AddModelError(key, string.Join(",", value));
                }
            }
            else
            {
                var x = response.Error?.GetContentAsAsync<SingleErrorModel>().Result;
                if (x != null)
                {
                    modelState.AddModelError("", x.ErrorMessage);
                }
                else
                {
                    if (error?.Title != null) modelState.AddModelError("", error.Title);
                }
            }
        }
        else
        {
            if (response.Error?.Message != null) modelState.AddModelError("", response.Error.Message);
        }
    }
}