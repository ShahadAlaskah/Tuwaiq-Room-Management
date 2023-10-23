using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Ef.Helpers;

public static class UtcExtensions
{
    private static DateTime FormCodeToData(DateTime fromCode, string name)
        => fromCode.Kind == DateTimeKind.Utc ? fromCode : throw new InvalidOperationException($"Column {name} only accepts UTC date-time values");

    private static DateTime FormDataToCode(DateTime fromData) 
        => fromData.Kind == DateTimeKind.Unspecified ? DateTime.SpecifyKind(fromData, DateTimeKind.Utc) : fromData.ToUniversalTime();

    private static DateTimeOffset FormCodeToData(DateTimeOffset fromCode, string name)
        => fromCode.Offset == TimeSpan.Zero ? fromCode : throw new InvalidOperationException($"Column {name} only accepts UTC date-time values");
    
    private static DateTimeOffset FormDataToCode(DateTimeOffset fromData)
        => fromData.Offset == TimeSpan.Zero ? fromData : fromData.ToOffset(TimeSpan.Zero);
    
    public static PropertyBuilder<DateTime?> UseUtc(this PropertyBuilder<DateTime?> property)
    {
        var name = property.Metadata.Name;
        return property.HasConversion<DateTime?>(
            fromCode => fromCode != null ? FormCodeToData(fromCode.Value, name) : default,
            fromData => fromData != null ? FormDataToCode(fromData.Value) : default
        );
    }
    
    public static PropertyBuilder<DateTime> UseUtc(this PropertyBuilder<DateTime> property)
    {
        var name = property.Metadata.Name;
        return property.HasConversion(fromCode => FormCodeToData(fromCode, name), fromData => FormDataToCode(fromData));
    }
    
    public static PropertyBuilder<DateTimeOffset?> UseUtc(this PropertyBuilder<DateTimeOffset?> property)
    {
        var name = property.Metadata.Name;
        return property.HasConversion<DateTimeOffset?>(
            fromCode => fromCode != null ? FormCodeToData(fromCode.Value, name) : default,
            fromData => fromData != null ? FormDataToCode(fromData.Value) : default
        );
    }
}