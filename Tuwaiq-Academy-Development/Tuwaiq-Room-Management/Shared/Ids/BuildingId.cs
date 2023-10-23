using StronglyTypedIds;

namespace Shared.Ids;

[StronglyTypedId(StronglyTypedIdBackingType.Guid,
    StronglyTypedIdConverter.TypeConverter | StronglyTypedIdConverter.NewtonsoftJson | StronglyTypedIdConverter.SystemTextJson)]
public readonly partial struct BuildingId
{
}