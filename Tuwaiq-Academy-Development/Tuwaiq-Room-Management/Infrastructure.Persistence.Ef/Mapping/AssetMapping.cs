using Domain.Domains;
using Infrastructure.Persistence.Ef.Converters;
using Infrastructure.Persistence.Ef.Converters.IdsConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Helpers;

namespace Infrastructure.Persistence.Ef.Mapping;

public class AssetMapping : IEntityTypeConfiguration<Asset>
{
    public void Configure(EntityTypeBuilder<Asset> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id)
            .HasConversion<AssetIdConverter>();
        
        builder.Property(s => s.RoomId)
            .HasConversion<RoomIdConverter>()
            ;
        
        builder.Property(s => s.AssetTypeId)
            .HasConversion<AssetTypeIdConverter>()
            ;
        
        builder.Property(x => x.Code).IsRequired().HasMaxLength(70);
        builder.Property(x => x.InstalledDate).HasConversion<DateOnlyConverter, DateOnlyComparer>();

        builder.HasOne(x => x.AssetType).WithMany().HasForeignKey(x => x.AssetTypeId);
        builder.HasOne(x => x.Room).WithMany().HasForeignKey(x => x.RoomId);
    }
}