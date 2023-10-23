using Application.Settings;
using Domain.Base;
using Domain.Domains;

namespace Application.Interfaces;

public interface IUnitOfWork
{
    
    object DbContext { get; }

    RoomManagementCacheSettings CacheOptions { get; }

    //object AuditViewsDbContext { get; }
    IGenericRepositoryCurd<Audit>? Audits { get; }
    IGenericRepositoryCurd<AuditView>? AuditViews { get; }

    IGenericRepositoryCurd<AssetType>? AssetTypes { get; }
    IGenericRepository<Asset>? Assets { get; }
    IGenericRepositoryCurd<Building>? Buildings { get; }
    IGenericRepositoryCurd<Floor>? Floors { get; }
    IGenericRepositoryCurd<Room>? Rooms { get; }
    IGenericRepositoryCurd<RoomType>? RoomTypes { get; }


    void Save();

    Task SaveChangesAsync(CancellationToken? cancellationToken = null);
}