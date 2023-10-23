using System.Diagnostics.CodeAnalysis;
using Shared.Domain;

namespace Shared.ValueObjects;

public class Attachment : ValueObject
{
    [SetsRequiredMembers]
    public Attachment(string fileName, string fileType, long fileSize)
    {
        FileName = fileName;
        FileType = fileType;
        FileSize = fileSize;
    }


    public required string FileName { get; init; }
    public required string FileType { get; init; }
    public required long FileSize { get; init; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return FileName;
        yield return FileType;
        yield return FileSize;
    }
}