using Domain.Domains;
using Infrastructure.Persistence.Ef.Converters.IdsConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Helpers;

namespace Infrastructure.Persistence.Ef.Mapping;

public class FloorMapping : IEntityTypeConfiguration<Floor>
{
    public void Configure(EntityTypeBuilder<Floor> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id)
            .HasConversion<FloorIdConverter>();

        builder.Property(x => x.Name).IsRequired().HasMaxLength(250);
    }
}