using Domain.Domains;
using Infrastructure.Persistence.Ef.Converters.IdsConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Helpers;

namespace Infrastructure.Persistence.Ef.Mapping;

public class AssetTypeMapping : IEntityTypeConfiguration<AssetType>
{
    public void Configure(EntityTypeBuilder<AssetType> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id)
            .HasConversion<AssetTypeIdConverter>();

        builder.Property(x => x.Name).IsRequired().HasMaxLength(250);
        builder.Property(x => x.Icon).HasMaxLength(300);
    }
}