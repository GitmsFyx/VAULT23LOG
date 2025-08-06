using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace LOG.Services;

public class LogDbContextFactory : IDesignTimeDbContextFactory<LogDbContext>
{
    public LogDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder =new DbContextOptionsBuilder<LogDbContext>();
        optionsBuilder.UseSqlite("Data Source=log.db");
        return new LogDbContext(optionsBuilder.Options);
    }
}