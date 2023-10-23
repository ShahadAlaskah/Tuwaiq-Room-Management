using Domain.Domains;
using Infrastructure.Persistence.Ef.Converters.IdsConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Helpers;

namespace Infrastructure.Persistence.Ef.Mapping;

public class RoomTypeMapping : IEntityTypeConfiguration<RoomType>
{
    public void Configure(EntityTypeBuilder<RoomType> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id)
            .HasConversion<RoomTypeIdConverter>();

        builder.Property(x => x.Name).IsRequired().HasMaxLength(250);
        builder.Property(x => x.RoomTypeEnum).IsRequired();
    }
}