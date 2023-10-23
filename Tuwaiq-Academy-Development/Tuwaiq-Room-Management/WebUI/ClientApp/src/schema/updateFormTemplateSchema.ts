import {z} from "zod";

export const updateFormTemplateSchema = z.object({
    title: z.string({required_error: "يجب ادخال العنوان"}).min(2, {message: "يجب أن يحتوي العنوان على الأقل 2 أحرف."}).max(100, {message: "يجب ألا يزيد العنوان عن 100 حرف."}),
    description: z.string().min(2, {message: "يجب ان يحتوى الوصف على الأقل 2 حرف"}).max(1000, {message: "يجب ألا يزيد الوصف عن 1000 حرف."}).or(z.null()).or(z.literal('')),
    questionOptionNumbers: z.object({
        i: z.array(z.object({
            title: z.string().min(2, 'يجب ان يحتوى السؤال على الأقل 2 حرف').max(100, "يجب ألا يزيد السؤال عن 100 حرف."),
            description: z.string().min(2, 'يجب ان يحتوى الوصف على الأقل 2 حرف').max(1000, "يجب ألا يزيد الوصف عن 1000 حرف.").or(z.null()).or(z.literal("")),
            label: z.string().min(2, "يجب ان يحتوى العنوان على الأقل 2 حرف").max(100, 'يجب ألا يزيد العنوان عن 100 حرف').or(z.null()).or(z.literal("")),
            placeHolder: z.string().min(2, "يجب ان يحتوى حقل الادخال على الأقل 2 حرف").max(100, 'يجب ألا يزيد حقل الادخال عن 100 حرف').or(z.null()).or(z.literal("")),
            min: z.number().min(0).optional(),
            max: z.number().min(0).optional(),
            step: z.number().min(0).optional(),
            defaultValue: z.string().min(2, 'يجب ان يحتوى القيمة الافتراضية على الأقل 2 حرف').max(100, 'يجب ألا يزيد القيمة الافتراضية عن 100 حرف').or(z.null()).or(z.literal("")),
        })).optional()
    }),
    questionOptionTexts: z.record(z.object({
            title: z.string().min(2, 'يجب ان يحتوى السؤال على الأقل 2 حرف').max(100, "يجب ألا يزيد السؤال عن 100 حرف."),
            description: z.string().min(2, 'يجب ان يحتوى الوصف على الأقل 2 حرف').max(1000, "يجب ألا يزيد الوصف عن 1000 حرف.").or(z.null()).or(z.literal("")),
            label: z.string().min(2, "يجب ان يحتوى العنوان على الأقل 2 حرف").max(100, 'يجب ألا يزيد العنوان عن 100 حرف').or(z.null()).or(z.literal("")),
            placeHolder: z.string().min(2, "يجب ان يحتوى حقل الادخال على الأقل 2 حرف").max(100, 'يجب ألا يزيد حقل الادخال عن 100 حرف').or(z.null()).or(z.literal("")),
            min: z.number().nullable().optional(),
            max: z.number().nullable().optional(),
            step: z.number().nullable().optional(),
            defaultValue: z.string().min(2, 'يجب ان يحتوى القيمة الافتراضية على الأقل 2 حرف').max(100, 'يجب ألا يزيد القيمة الافتراضية عن 100 حرف').or(z.null()).or(z.literal("")),
        })
    ),
    questionOptionDates: z.object({
        i: z.array(z.object({
            title: z.string().min(2, 'يجب ان يحتوى السؤال على الأقل 2 حرف').max(100, "يجب ألا يزيد السؤال عن 100 حرف."),
            description: z.string().min(2, 'يجب ان يحتوى الوصف على الأقل 2 حرف').max(1000, "يجب ألا يزيد الوصف عن 1000 حرف.").or(z.null()).or(z.literal("")),
            label: z.string().min(2, "يجب ان يحتوى العنوان على الأقل 2 حرف").max(100, 'يجب ألا يزيد العنوان عن 100 حرف').or(z.null()).or(z.literal("")),
            placeHolder: z.string().min(2, "يجب ان يحتوى حقل الادخال على الأقل 2 حرف").max(100, 'يجب ألا يزيد حقل الادخال عن 100 حرف').or(z.null()).or(z.literal("")),
            min: z.string().nullable().optional(),
            max: z.string().nullable().optional(),
            step: z.number().nullable().optional(),
            defaultValue: z.string().min(2, 'يجب ان يحتوى القيمة الافتراضية على الأقل 2 حرف').max(100, 'يجب ألا يزيد القيمة الافتراضية عن 100 حرف').or(z.null()).or(z.literal("")),
        })).optional()
    }),
    questionOptionMultipleChoices: z.object({
        i: z.array(z.object({
            title: z.string().min(2, 'يجب ان يحتوى السؤال على الأقل 2 حرف').max(100, "يجب ألا يزيد السؤال عن 100 حرف."),
            description: z.string().min(2, 'يجب ان يحتوى الوصف على الأقل 2 حرف').max(1000, "يجب ألا يزيد الوصف عن 1000 حرف.").or(z.null()).or(z.literal("")),
            label: z.string().min(2, "يجب ان يحتوى العنوان على الأقل 2 حرف").max(100, 'يجب ألا يزيد العنوان عن 100 حرف').or(z.null()).or(z.literal("")),
            placeHolder: z.string().min(2, "يجب ان يحتوى حقل الادخال على الأقل 2 حرف").max(100, 'يجب ألا يزيد حقل الادخال عن 100 حرف').or(z.null()).or(z.literal("")),
            options: z.array(z.string().min(1).max(100)).min(2, 'يجب اضافة سؤال واحد على الاقل'),
            defaultValue: z.string().min(2, 'يجب ان يحتوى القيمة الافتراضية على الأقل 2 حرف').max(100, 'يجب ألا يزيد القيمة الافتراضية عن 100 حرف').or(z.null()).or(z.literal("")),
        })).optional()
    }),
    questionOptionFiles: z.object({
        i: z.array(z.object({
            title: z.string().min(2, 'يجب ان يحتوى السؤال على الأقل 2 حرف').max(100, "يجب ألا يزيد السؤال عن 100 حرف."),
            description: z.string().min(2, 'يجب ان يحتوى الوصف على الأقل 2 حرف').max(1000, "يجب ألا يزيد الوصف عن 1000 حرف.").or(z.null()).or(z.literal("")),
            label: z.string().min(2, "يجب ان يحتوى العنوان على الأقل 2 حرف").max(100, 'يجب ألا يزيد العنوان عن 100 حرف').or(z.null()).or(z.literal("")),
            placeHolder: z.string().min(2, "يجب ان يحتوى حقل الادخال على الأقل 2 حرف").max(100, 'يجب ألا يزيد حقل الادخال عن 100 حرف').or(z.null()).or(z.literal("")),
            defaultValue: z.string().min(2, 'يجب ان يحتوى القيمة الافتراضية على الأقل 2 حرف').max(100, 'يجب ألا يزيد القيمة الافتراضية عن 100 حرف').or(z.null()).or(z.literal("")),
        })).optional()
    }),


});