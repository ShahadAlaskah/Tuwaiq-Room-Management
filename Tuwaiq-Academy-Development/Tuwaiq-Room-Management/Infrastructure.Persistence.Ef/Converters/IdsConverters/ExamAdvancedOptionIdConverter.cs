using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Shared.Ids;

namespace Infrastructure.Persistence.Ef.Converters.IdsConverters;

public class ExamAdvancedOptionIdConverter : ValueConverter<ExamAdvancedOptionId, Guid>
{
    public ExamAdvancedOptionIdConverter() : base(c => c.Value, c => new(c))
    {
    }
}