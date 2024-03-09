//using Microsoft.EntityFrameworkCore.Design;
//using Microsoft.EntityFrameworkCore;
//using VolunteerTracker.Blazor.Data;

//namespace VolunteerTracker.Blazor.Data;

//public class AspNetVolunteerContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
//{
//    public ApplicationDbContext CreateDbContext(string[] args)
//    {
//        var connectionString = Environment.GetEnvironmentVariable("EFMIGRATIONDB");

//        if (string.IsNullOrEmpty(connectionString))
//            throw new InvalidOperationException(
//                "The connection string was not set in the 'EFMIGRATIONDB' environment variable.");

//        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>().UseNpgsql(connectionString,
//            b => b.MigrationsAssembly("VolunteerTracker.Blazor"));

//        return new ApplicationDbContext(optionsBuilder.Options);
//    }
//}