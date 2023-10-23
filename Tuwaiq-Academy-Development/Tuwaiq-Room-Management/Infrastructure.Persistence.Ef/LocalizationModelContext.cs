// using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.Options;
//
// namespace API.Infrastructure.Persistence.Ef;
//
// public class LocalizationRecord
// {
//     public long Id { get; set; }
//     public string Key { get; set; } = null!;
//     public string Text { get; set; } = null!;
//     public string LocalizationCulture { get; set; } = null!;
//     public string ResourceKey { get; set; } = null!;
// }
//
// public class SqlContextOptions
// {
//     /// <summary>
//     /// SQL Server schema on which the tables are supposed to be created, if none, database default will be used
//     /// </summary>
//     public string SqlSchemaName { get; set; }
//
// }
//
// public class LocalizationModelContext : DbContext
//     {
//         private string _schema;
//
//         public LocalizationModelContext(DbContextOptions<LocalizationModelContext> options, IOptions<SqlContextOptions> contextOptions) : base(options)
//         {
//             _schema = contextOptions.Value.SqlSchemaName;
//         }
//
//         public DbSet<LocalizationRecord> LocalizationRecords { get; set; }
//         // public DbSet<ExportHistory> ExportHistoryDbSet { get; set; }
//         // public DbSet<ImportHistory> ImportHistoryDbSet { get; set; }
//
//
//         protected override void OnModelCreating(ModelBuilder builder)
//         {
//             if (!string.IsNullOrEmpty(_schema))
//                 builder.HasDefaultSchema(_schema);
//             builder.Entity<LocalizationRecord>().HasKey(m => m.Id);
//             builder.Entity<LocalizationRecord>().HasAlternateKey(c => new { c.Key, c.LocalizationCulture, c.ResourceKey });
//
//             // shadow properties
//             builder.Entity<LocalizationRecord>().Property<DateTime>("UpdatedTimestamp");
//
//             // builder.Entity<ExportHistory>().HasKey(m => m.Id);
//             //
//             // builder.Entity<ImportHistory>().HasKey(m => m.Id);
//
//             base.OnModelCreating(builder);
//         }
//
//         public override int SaveChanges()
//         {
//             ChangeTracker.DetectChanges();
//             updateUpdatedProperty<LocalizationRecord>();
//             return base.SaveChanges();
//         }
//
//         private void updateUpdatedProperty<T>() where T : class
//         {
//             var modifiedSourceInfo =
//                 ChangeTracker.Entries<T>()
//                     .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);
//
//             foreach (var entry in modifiedSourceInfo)
//             {
//                 entry.Property("UpdatedTimestamp").CurrentValue = DateTime.UtcNow;
//             }
//         }
//
//         public void DetachAllEntities()
//         {
//             var changedEntriesCopy = ChangeTracker.Entries().ToList();
//             foreach (var entry in changedEntriesCopy)
//                 entry.State = EntityState.Detached;
//         }
//     }