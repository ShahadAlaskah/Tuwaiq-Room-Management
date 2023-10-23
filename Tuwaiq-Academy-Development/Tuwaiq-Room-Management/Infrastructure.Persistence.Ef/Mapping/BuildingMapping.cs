using Domain.Domains;
using Infrastructure.Persistence.Ef.Converters.IdsConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Helpers;

namespace Infrastructure.Persistence.Ef.Mapping;

public class BuildingMapping : IEntityTypeConfiguration<Building>
{
    public void Configure(EntityTypeBuilder<Building> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id)
            .HasConversion<BuildingIdConverter>();

        builder.Property(x => x.Name).IsRequired().HasMaxLength(250);
        builder.Property(x => x.Code).IsRequired().HasMaxLength(70);
    }
}