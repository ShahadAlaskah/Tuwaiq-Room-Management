using StronglyTypedIds;

namespace Shared.Ids;

[StronglyTypedId(StronglyTypedIdBackingType.String,
    StronglyTypedIdConverter.TypeConverter | StronglyTypedIdConverter.NewtonsoftJson | StronglyTypedIdConverter.SystemTextJson)]
public readonly partial struct UserId
{
}
[StronglyTypedId(StronglyTypedIdBackingType.Guid,
    StronglyTypedIdConverter.TypeConverter | StronglyTypedIdConverter.NewtonsoftJson | StronglyTypedIdConverter.SystemTextJson)]
public readonly partial struct UserProfileId
{
}


[StronglyTypedId(StronglyTypedIdBackingType.Guid,
    StronglyTypedIdConverter.TypeConverter | StronglyTypedIdConverter.NewtonsoftJson | StronglyTypedIdConverter.SystemTextJson)]
public readonly partial struct FormSubmissionId
{
}

