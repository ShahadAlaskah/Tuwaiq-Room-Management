using Domain.Domains;
using Infrastructure.Persistence.Ef.Converters.IdsConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Helpers;

namespace Infrastructure.Persistence.Ef.Mapping;

public class RoomMapping : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id)
            .HasConversion<RoomIdConverter>();

        builder.Property(x => x.Name).IsRequired().HasMaxLength(70);
        builder.Property(x => x.Code).IsRequired().HasMaxLength(70);
        builder.Property(x => x.Capacity).IsRequired();

        builder.HasOne(x => x.RoomType).WithMany().HasForeignKey(x => x.RoomTypeId);
        builder.HasOne(x => x.Floor).WithMany().HasForeignKey(x => x.FloorId);
        builder.HasOne(x => x.Building).WithMany().HasForeignKey(x => x.BuildingId);

        builder.Metadata.FindNavigation(nameof(Room.Assets))!.SetPropertyAccessMode(PropertyAccessMode.Field);


        builder.Ignore(d => d.DomainEvents);
        builder.Ignore(d => d.DomainNotifications);
    }
}