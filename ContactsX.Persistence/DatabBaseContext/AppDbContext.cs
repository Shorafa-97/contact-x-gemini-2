using ContactsX.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace ContactsX.Persistence.DatabBaseContext;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Entity> Entities { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Relation> Relations { get; set; }
    public DbSet<DuplicateCandidate> DuplicateCandidates { get; set; }
    public DbSet<AuditLog> AuditLogs { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}