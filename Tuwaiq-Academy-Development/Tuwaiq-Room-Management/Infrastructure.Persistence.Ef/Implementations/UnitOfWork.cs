using Application.Interfaces;
using Application.Settings;
using Domain.Base;
using Domain.Domains;
using Microsoft.Extensions.Options;

namespace Infrastructure.Persistence.Ef.Implementations;

public class UnitOfWork : IDisposable, IUnitOfWork
{
    // private readonly DbContextFactory _contextFactory;

    private bool _disposed;

    public UnitOfWork( //DbContextFactory contextFactory,
        ApplicationDbContext context,
        IOptionsMonitor<RoomManagementCacheSettings> cacheOptions)
    {
        // _contextFactory = contextFactory;
        DbContext = context;
        CacheOptions = cacheOptions.CurrentValue;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public RoomManagementCacheSettings CacheOptions { get; }


    public object DbContext { get; set; }

    private IGenericRepositoryCurd<AuditView>? _auditViews;


    public IGenericRepositoryCurd<AuditView> AuditViews
    {
        get
        {
            // var baseContext = _contextFactory.GetContext(DbContext as ApplicationDbContext);
            // DbContext = baseContext;
            _auditViews ??= new GenericRepositoryCurd<AuditView>((DbContext as ApplicationDbContext)!);
            return _auditViews!;
        }
    }
    
    private IGenericRepositoryCurd<Audit>? _audits;

    public IGenericRepositoryCurd<Audit> Audits
    {
        get
        {
            _audits ??= new GenericRepositoryCurd<Audit>((DbContext as ApplicationDbContext)!);
            return _audits!;
        }
    }

    private IGenericRepositoryCurd<AssetType>? _assetTypes;

    public IGenericRepositoryCurd<AssetType> AssetTypes
    {
        get
        {
            _assetTypes ??= new GenericRepositoryCurd<AssetType>((DbContext as ApplicationDbContext)!);
            return _assetTypes!;
        }
    }

    private IGenericRepository<Asset>? _assets;

    public IGenericRepository<Asset> Assets
    {
        get
        {
            _assets ??= new GenericRepository<Asset>((DbContext as ApplicationDbContext)!);
            return _assets!;
        }
    }

    private IGenericRepositoryCurd<Room>? _rooms;

    public IGenericRepositoryCurd<Room> Rooms
    {
        get
        {
            _rooms ??= new GenericRepositoryCurd<Room>((DbContext as ApplicationDbContext)!);
            return _rooms!;
        }
    }

    private IGenericRepositoryCurd<RoomType>? _roomTypes;

    public IGenericRepositoryCurd<RoomType> RoomTypes
    {
        get
        {
            _roomTypes ??= new GenericRepositoryCurd<RoomType>((DbContext as ApplicationDbContext)!);
            return _roomTypes!;
        }
    }

    private IGenericRepositoryCurd<Floor>? _floors;

    public IGenericRepositoryCurd<Floor> Floors
    {
        get
        {
            _floors ??= new GenericRepositoryCurd<Floor>((DbContext as ApplicationDbContext)!);
            return _floors!;
        }
    }

    private IGenericRepositoryCurd<Building>? _buildings;

    public IGenericRepositoryCurd<Building> Buildings
    {
        get
        {
            _buildings ??= new GenericRepositoryCurd<Building>((DbContext as ApplicationDbContext)!);
            return _buildings!;
        }
    }


    public void Save()
    {
        (DbContext as ApplicationDbContext)?.SaveChanges();
        // if (DbContext is ApplicationDbContext)
        // {
        //     (DbContext as ApplicationDbContext)?.SaveChanges();
        // }
        //
        // if (DbContext is AudiViewDbContext)
        // {
        //     (DbContext as AudiViewDbContext)?.SaveChanges();
        // }
    }

    public Task SaveChangesAsync(CancellationToken? cancellationToken = null)
    {
        return (DbContext as ApplicationDbContext)!.SaveChangesAsync(true, cancellationToken ?? default);

        // if (DbContext is ApplicationDbContext)
        // {
        //     return ((DbContext as ApplicationDbContext)!).SaveChangesAsync(true, cancellationToken ?? default);
        // }
        //
        // if (DbContext is AudiViewDbContext)
        // {
        //     return (DbContext as AudiViewDbContext)?.SaveChangesAsync(true, cancellationToken ?? default)!;
        // }
        //
        // return (DbContext as BaseContext)!.SaveChangesAsync(true, cancellationToken ?? default);
    }

    private void Dispose(bool disposing)
    {
        if (!_disposed)
            if (disposing)
                try
                {
                    (DbContext as ApplicationDbContext)?.Dispose();
                }
                catch
                {
                    // ignored
                }

        //
        // try
        // {
        //     (DbContext as AudiViewDbContext)?.Dispose();
        // }
        // catch
        // {
        //     // ignored
        // }
        _disposed = true;
    }
}