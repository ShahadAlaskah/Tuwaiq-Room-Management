// using Application.Dto;
// using Application.Persistence.Queries;
// using Application.Specifications;
// using Domain.Enums;
// using MediatR;
// using Shared.Base;
// using Shared.Interfaces;
//
// namespace Infrastructure.Persistence.QueryHandlers;
//
// public class GetDashboardQueryHandler : IQueryHandler<GetDashboardQuery, DashboardResponse>
// {
//     private readonly ISender _sender;
//
//     public GetDashboardQueryHandler(ISender sender)
//     {
//         _sender = sender;
//     }
//
//     public async Task<Result<DashboardResponse>> Handle(GetDashboardQuery request, CancellationToken cancellationToken)
//     {
//         var response = new DashboardResponse();
//
//         var categories =
//             await _sender.Send(new GetFormTemplateCategoryQuery(new GetFormTemplateCategoriesSpecification(), 1, 1000000000),
//                 cancellationToken);
//         var forms = await _sender.Send(new GetFormTemplateQuery(new GetFormTemplatesSpecification(), 1, 1000000000),
//             cancellationToken);
//
//
//         response.FormTemplateCategoryCount = categories.Value.TotalCount;
//         response.FormTemplateCategoryBootcampFormsCount =
//             categories.Value.Count(s => s.CategoryType == CategoryType.BootcampForms);
//         response.FormTemplateCategoryOthersCount = categories.Value.Count(s => s.CategoryType == CategoryType.Other);
//         response.FormTemplateCategoryExamsFormsCount = categories.Value.Count(s => s.CategoryType == CategoryType.ExamsForms);
//
//
//         response.FormTemplateCount = forms.Value.TotalCount;
//         response.FormTemplateBootcampFormsCount =
//             forms.Value.Count(s => s.FormTemplateCategory?.CategoryType == CategoryType.BootcampForms);
//         response.FormTemplateOthersCount = forms.Value.Count(s => s.FormTemplateCategory?.CategoryType == CategoryType.Other);
//         response.FormTemplateExamsFormsCount =
//             forms.Value.Count(s => s.FormTemplateCategory?.CategoryType == CategoryType.ExamsForms);
//
//
//         response.FormTemplateBootcampFormsActiveCount = forms.Value.Count(s =>
//             s.FormTemplateCategory?.CategoryType == CategoryType.BootcampForms && s.IsActive);
//         response.FormTemplateOthersActiveCount =
//             forms.Value.Count(s => s.FormTemplateCategory?.CategoryType == CategoryType.Other && s.IsActive);
//         response.FormTemplateExamsFormsActiveCount =
//             forms.Value.Count(s => s.FormTemplateCategory?.CategoryType == CategoryType.ExamsForms && s.IsActive);
//
//
//         return Result.Ok(response);
//     }
// }