using LOG.Models;
using Microsoft.EntityFrameworkCore;

namespace LOG.Services;

public class LogDbContext :DbContext
{
    public DbSet<Log> Logs { get; set; }
    public DbSet<People> Peoples { get; set; }
    
    public DbSet<Photo> Photos { get; set; }
    
    public LogDbContext(DbContextOptions<LogDbContext> options) : base(options)
    {
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=log.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<People>()
            .HasMany(p=>p.Logs)
            .WithOne(l=>l.People)
            .HasForeignKey(l=>l.PeopleId)
            .IsRequired();

        modelBuilder.Entity<People>()
            .HasMany(p => p.Photos)
            .WithOne(p => p.People)
            .HasForeignKey(p => p.PeopleId)
            .IsRequired();
    }
}