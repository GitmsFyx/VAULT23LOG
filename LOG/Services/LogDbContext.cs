using LOG.Models;
using Microsoft.EntityFrameworkCore;

namespace LOG.Services;

public class LogDbContext :DbContext
{
    public DbSet<Log> Logs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=log.db");
    }
}