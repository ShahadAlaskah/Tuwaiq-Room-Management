﻿export enum api {
    GetPartners = 'Lookups/GetPartners',
    GetFormTemplateCategoriesLookup = 'Lookups/GetFormTemplateCategories',
    Upload = 'Files/Upload',
    GetForms = 'API/GetForms',
    GetForm = 'API/GetForm',
    CreateForm = 'API/CreateForm',
    GetFormTemplateCategories = 'FormTemplateCategories/GetAll',
    DeleteFormTemplateCategories = 'FormTemplateCategories/Delete/',
    GetFormTemplateSections = 'FormTemplateSections/GetAll',
    DeleteFormTemplateSections = 'FormTemplateSections/Delete/',
    GetFormTemplates = 'FormTemplates/GetAll',
    GetFormTemplatesByCategory = 'FormTemplates/GetByCategory',
    DeleteFormTemplates = 'API/DeleteFormTemplate/',
    CreateFormTemplate = 'API/CreateFormTemplate',
    UpdateFormTemplate = 'API/UpdateFormTemplate',
    SetActiveFormTemplate = 'API/Active',
    PublishFormTemplate = 'API/PublishFormTemplate/',
    UnPublishFormTemplate = 'API/UnPublishFormTemplate/'
}
