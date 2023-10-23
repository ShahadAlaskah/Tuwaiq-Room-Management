using Mapster;
using Shared.Ids;

namespace Shared.Domain;

public abstract class BaseDto<TDto, TEntity> : IRegister, IBaseNotificationServiceDto
    where TDto : class
    where TEntity : class
{
    private TypeAdapterConfig Config { get; set; } = null!;

    public void Register(TypeAdapterConfig config)
    {
        config.ForType<UserId, string>().MapWith(d => d.Value);
        config.ForType<string, UserId>().MapWith(f => new(f));

        
        config.ForType<AssetTypeId, Guid>().MapWith(d => d.Value);
        config.ForType<Guid, AssetTypeId>().MapWith(f => new(f));
        
        config.ForType<AssetTypeId?, Guid?>().MapWith(d => d.HasValue ? d.Value.Value : Guid.Empty);
        config.ForType<Guid?, AssetTypeId?>()
            .MapWith(f => f.HasValue ? new(f.Value) : AssetTypeId.Empty);

        
        config.ForType<AssetId, Guid>().MapWith(d => d.Value);
        config.ForType<Guid, AssetId>().MapWith(f => new(f));
        
        config.ForType<AssetId?, Guid?>().MapWith(d => d.HasValue ? d.Value.Value : Guid.Empty);
        config.ForType<Guid?, AssetId?>()
            .MapWith(f => f.HasValue ? new(f.Value) : AssetId.Empty);

        
        config.ForType<BuildingId, Guid>().MapWith(d => d.Value);
        config.ForType<Guid, BuildingId>().MapWith(f => new(f));
        
        config.ForType<BuildingId?, Guid?>().MapWith(d => d.HasValue ? d.Value.Value : Guid.Empty);
        config.ForType<Guid?, BuildingId?>()
            .MapWith(f => f.HasValue ? new(f.Value) : BuildingId.Empty);

        
        config.ForType<FloorId, Guid>().MapWith(d => d.Value);
        config.ForType<Guid, FloorId>().MapWith(f => new(f));

        config.ForType<FloorId?, Guid?>().MapWith(d => d.HasValue ? d.Value.Value : Guid.Empty);
        config.ForType<Guid?, FloorId?>()
            .MapWith(f => f.HasValue ? new(f.Value) : FloorId.Empty);

        
        config.ForType<RoomId, Guid>().MapWith(d => d.Value);
        config.ForType<Guid, RoomId>().MapWith(f => new(f));
        
        config.ForType<RoomId?, Guid?>().MapWith(d => d.HasValue ? d.Value.Value : Guid.Empty);
        config.ForType<Guid?, RoomId?>()
            .MapWith(f => f.HasValue ? new(f.Value) : RoomId.Empty);

        
        config.ForType<RoomTypeId, Guid>().MapWith(d => d.Value);
        config.ForType<Guid, RoomTypeId>().MapWith(f => new(f));
        
        config.ForType<RoomTypeId?, Guid?>().MapWith(d => d.HasValue ? d.Value.Value : Guid.Empty);
        config.ForType<Guid?, RoomTypeId?>()
            .MapWith(f => f.HasValue ? new(f.Value) : RoomTypeId.Empty);

        
        // config.ForType<Email, string>().MapWith(d => d.EmailAddress);
        // config.ForType<string, Email>().ConstructUsing(f => new Email(f));
        //
        // config.ForType<Mobile, string>().MapWith(d => d.Value);
        // config.ForType<string, Mobile>().ConstructUsing(f => new Mobile(f));
        
        
        config.ForType<AuditId, Guid>().MapWith(d => d.Value);
        config.ForType<Guid, AuditId>().MapWith(f => new AuditId(f));

        config.ForType<AuditViewId, Guid>().MapWith(d => d.Value);
        config.ForType<Guid, AuditViewId>().MapWith(f => new AuditViewId(f));

        
        Config = config;

        AddCustomMappings();
    }

    public TEntity ToEntity()
    {
        return this.Adapt<TEntity>();
    }

    // ReSharper disable once UnusedMember.Global
    public TEntity ToEntity(TEntity entity)
    {
        return (this as TDto).Adapt(entity);
    }

    // ReSharper disable once UnusedMember.Global
    public static TDto FromEntity(TEntity entity)
    {
        return entity.Adapt<TDto>();
    }

    // ReSharper disable once VirtualMemberNeverOverridden.Global
    protected virtual void AddCustomMappings()
    {
    }


    // ReSharper disable once UnusedMember.Global
    protected TypeAdapterSetter<TDto, TEntity> SetCustomMappings()
    {
        return Config.ForType<TDto, TEntity>();
    }

    // ReSharper disable once UnusedMember.Global
    protected TypeAdapterSetter<TEntity, TDto> SetCustomMappingsInverse()
    {
        return Config.ForType<TEntity, TDto>();
    }
}