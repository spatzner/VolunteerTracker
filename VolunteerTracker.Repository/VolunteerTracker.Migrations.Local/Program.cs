



using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using VolunteerTracker.Repository;

public class VolunteerContextFactory : IDesignTimeDbContextFactory<VolunteerContext>
{
    public VolunteerContext CreateDbContext(string[] args)
    {
        var connectionString = Environment.GetEnvironmentVariable("EFMIGRATIONDB");
        
        if (string.IsNullOrEmpty(connectionString))
            throw new InvalidOperationException(
                "The connection string was not set in the 'EFMIGRATIONDB' environment variable.");

        var optionsBuilder = new DbContextOptionsBuilder<VolunteerContext>().UseNpgsql(connectionString);

        return new VolunteerContext(optionsBuilder.Options);
    }
}