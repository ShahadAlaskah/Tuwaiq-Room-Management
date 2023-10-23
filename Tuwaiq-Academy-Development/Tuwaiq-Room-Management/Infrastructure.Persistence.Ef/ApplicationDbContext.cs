using AppAny.Quartz.EntityFrameworkCore.Migrations;
using AppAny.Quartz.EntityFrameworkCore.Migrations.MySql;
using Domain.Base;
using Domain.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.Ids;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Shared.Domain;
using Shared.Enums;
using Shared.Extensions;
using Infrastructure.Persistence.Ef.Converters.IdsConverters;
using Infrastructure.Persistence.Ef.Interceptors;
using Infrastructure.Persistence.Ef.Mapping;

namespace Infrastructure.Persistence.Ef;

public class ApplicationDbContext : DbContext, IDataProtectionKeyContext
{
    private readonly IDomainEventDispatcher _dispatcher;
    private readonly UserId _username;
    private readonly string _clientId;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IDomainEventDispatcher dispatcher,
        IHttpContextAccessor contextAccessor)
        : base(options)
    {
        _dispatcher = dispatcher;
        //ChangeTracker.LazyLoadingEnabled = false;
        _username = contextAccessor.HttpContext?.User.Identity != null && contextAccessor.HttpContext != null &&
                    contextAccessor.HttpContext.User.Identity.IsAuthenticated
            ? new UserId(contextAccessor.HttpContext.User.GetUserId().ToString())
            : UserId.Empty;
        _clientId = contextAccessor.HttpContext?.User.Identity != null && contextAccessor.HttpContext != null &&
                    contextAccessor.HttpContext.User.Identity.IsAuthenticated
            ? contextAccessor.HttpContext.User.GetClientId()
            : string.Empty;
    }


    public DbSet<AssetType>? AssetType { get; set; }

    public DbSet<Audit>? Audits { get; set; } = null!;
   // public DbSet<AuditLog> AuditLogs { get; set; } = null!;

    public Task<int> SaveChangesAsync()
    {
        return SaveChangesAsync(CancellationToken.None);
    }

    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = new CancellationToken())
    {
        await _preSaveChanges();

        var auditEntries = OnBeforeSaveChanges();
        var res = await base.SaveChangesAsync(true, cancellationToken);
        await OnAfterSaveChanges(auditEntries);

        return res;
    }

    // public DbSet<PersistedGrant> PersistedGrants { get; set; }
    // public DbSet<DeviceFlowCodes> DeviceFlowCodes { get; set; }

    // public DbSet<PersistedGrant> PersistedGrants { get; set; }
    //
    // public DbSet<DeviceFlowCodes> DeviceFlowCodes { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(new AuditReadInterceptor());

        // if (!optionsBuilder.IsConfigured)
        // {
        //     optionsBuilder.UseSqlServer(
        //             @"data source=localhost;initial catalog=API;UserIdentity Id=sa;Password=P@ssw0rd;TrustServerCertificate=true;",
        //             x =>
        //                 x.MigrationsAssembly("API.Infrastructure.Persistence.Ef")
        //                     .EnableRetryOnFailure(5, TimeSpan.FromSeconds(3), null).CommandTimeout(180))
        //         .EnableSensitiveDataLogging().EnableDetailedErrors()
        //         .EnableSensitiveDataLogging()
        //         .EnableThreadSafetyChecks();
        // }

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.AddQuartz(b => b.UseMySql());

        builder.Entity<Audit>(s =>
        {
            s.HasKey(d => d.Id);
            s.Property(d => d.Id)
                // .HasConversion(new ValueConverter<AuditId, Guid>(c => c.Value, c => new AuditId(c)));
                .HasConversion<AuditIdConverter>()
                // .HasConversion(new ValueConverter<AuditId, Guid>(c => c.Value, c => new(c)))
                // .HasValueGenerator<StronglyTypedIdValueGenerator<AuditId>>()
                ;
            s.Property(x => x.UserId)
                .HasConversion<UserIdConverter>();
        });

        builder.Entity<AuditView>(s =>
        {
            s.HasKey(d => d.Id);
            s.Property(d => d.Id)
                .HasConversion<AuditViewIdConverter>();

            s.Property(x => x.UserId).HasConversion<UserIdConverter>();
        });

        builder.Entity<AuditView>().Property(ae => ae.Tables)
            .HasConversion<AuditViewTablesConverter>()
            // .HasConversion(
            //     value => JsonConvert.SerializeObject(value),
            //     serializedValue => JsonConvert.DeserializeObject<string[]?>(serializedValue))
            ; //.Metadata.SetValueComparer(new StringArrayComparer());

        builder.Entity<AuditView>().Property(ae => ae.Args)
            .HasConversion<AuditViewArgsConverter>()
            // .HasConversion(
            //     value => JsonConvert.SerializeObject(value),
            //     serializedValue => JsonConvert.DeserializeObject<Dictionary<string, object>?>(serializedValue))
            ; //.Metadata.SetValueComparer(new DictionaryComparer());

        base.OnModelCreating(builder);


        builder.ApplyConfigurationsFromAssembly(typeof(AssetTypeMapping).Assembly);
    }

    private async Task _preSaveChanges()
    {
        await _dispatchDomainEvents();
    }

    private async Task _dispatchDomainEvents()
    {
        var domainEventEntities = ChangeTracker.Entries<IEntity>()
            .Select(po => po.Entity)
            .Where(po => po.DomainEvents.Any())
            .ToArray();

        foreach (var entity in domainEventEntities)
            while (entity.DomainEvents.TryTake(out var dev))
                await _dispatcher.DispatchEvent(dev);

        var domainIdentityEntities = ChangeTracker.Entries<IEntity>()
            .Select(po => po.Entity)
            .Where(po => po.DomainNotifications.Any())
            .ToArray();

        foreach (var entity in domainIdentityEntities)
            while (entity.DomainNotifications.TryTake(out var dev))
                await _dispatcher.DispatchNotification(dev);
    }

    public override int SaveChanges()
    {
        _preSaveChanges().GetAwaiter().GetResult();

        var auditEntries = OnBeforeSaveChanges();

        var res = base.SaveChanges();

        OnAfterSaveChanges(auditEntries).GetAwaiter().GetResult();

        return res;
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _preSaveChanges();

        var auditEntries = OnBeforeSaveChanges();
        var res = await base.SaveChangesAsync(true, cancellationToken);
        await OnAfterSaveChanges(auditEntries);

        return res;
    }


    private List<AuditEntry<EntityEntry, PropertyEntry>> OnBeforeSaveChanges()
    {
        ChangeTracker.DetectChanges();
        var auditEntries = new List<AuditEntry<EntityEntry, PropertyEntry>>();
        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.Entity is Audit || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged ||
                !(entry.Entity is IAudiTable))
                continue;

            var auditEntry = new AuditEntry<EntityEntry, PropertyEntry>(entry)
            {
                TableName = entry.Entity.GetType().Name,
                UserId = _username,
                ClientId = _clientId
            };
            auditEntries.Add(auditEntry);
            foreach (var property in entry.Properties)
            {
                if (property.IsTemporary)
                {
                    auditEntry.TemporaryProperties.Add(property);
                    continue;
                }

                var propertyName = property.Metadata.Name;
                if (property.Metadata.IsPrimaryKey())
                {
                    auditEntry.KeyValues[propertyName] = property.CurrentValue!;
                    continue;
                }

                switch (entry.State)
                {
                    case EntityState.Added:
                        auditEntry.AuditType = AuditType.Create;
                        auditEntry.NewValues[propertyName] = property.CurrentValue!;
                        break;

                    case EntityState.Deleted:
                        auditEntry.AuditType = AuditType.Delete;
                        auditEntry.OldValues[propertyName] = property.OriginalValue!;
                        break;

                    case EntityState.Modified:
                        if (property.IsModified)
                        {
                            auditEntry.ChangedColumns.Add(propertyName);
                            auditEntry.AuditType = AuditType.Update;
                            auditEntry.OldValues[propertyName] = property.OriginalValue!;
                            auditEntry.NewValues[propertyName] = property.CurrentValue!;
                        }

                        break;
                }
            }
        }

        foreach (var auditEntry in auditEntries.Where(_ => !_.HasTemporaryProperties))
            Audits?.Add(auditEntry.ToAudit());

        return auditEntries.Where(_ => _.HasTemporaryProperties).ToList();
    }

    private Task OnAfterSaveChanges(List<AuditEntry<EntityEntry, PropertyEntry>> auditEntries)
    {
        if (auditEntries.Count == 0)
            return Task.CompletedTask;

        foreach (var auditEntry in auditEntries)
        {
            foreach (var prop in auditEntry.TemporaryProperties)
                if (prop.Metadata.IsPrimaryKey())
                    auditEntry.KeyValues[prop.Metadata.Name] = prop.CurrentValue!;
                else
                    auditEntry.NewValues[prop.Metadata.Name] = prop.CurrentValue!;

           // if (AuditLogs != null) Audits.Add(auditEntry.ToAudit());
        }

        return SaveChangesAsync();
    }

    public DbSet<DataProtectionKey> DataProtectionKeys { get; set; } = null!;
    // public DbSet<Client> Clients { get; set; }
    // public DbSet<ClientCorsOrigin> ClientCorsOrigins { get; set; }
    // public DbSet<IdentityResource> IdentityResources { get; set; }
    // public DbSet<ApiResource> ApiResources { get; set; }
    // public DbSet<ApiScope> ApiScopes { get; set; }
}