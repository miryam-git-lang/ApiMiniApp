using Microsoft.EntityFrameworkCore;
using ApiMiniApp.Models;
using ApiMiniApp.Models.Common;

namespace ApiMiniApp.Data;

public class AppDbContext : DbContext
{
    public DbSet<Event> Events { get; set; }
    public DbSet<Ticket> Tickets { get; set; } 
    public DbSet<Organizer> Organizers { get; set; } 
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    override protected void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            base.OnModelCreating(builder);
        }
    
        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<AuditableEntity>();
            foreach(var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.Now;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = DateTime.Now;
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
    
        }
}