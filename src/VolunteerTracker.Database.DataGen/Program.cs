// See https://aka.ms/new-console-template for more information

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VolunteerTracker.Database.DataGen;
using VolunteerTracker.Repository;

var builder = Host
   .CreateDefaultBuilder(args)
   .ConfigureAppConfiguration((hostingContext, config) =>
    {
        config.AddJsonFile("appsettings.json", optional: false);
        config.AddUserSecrets<Program>();
    })
   .ConfigureServices((hostContext, services) =>
    {

        var databaseSettings = new VolunteerDatabaseSettings();
        hostContext.Configuration.GetSection("VolunteerDatabaseSettings").Bind(databaseSettings);

        services.AddDbContext<VolunteerContext>(options =>
        {
            options.UseNpgsql(databaseSettings.ConnectionString);
        });
        
        services.AddTransient<DataGenerator>();
    });

var app = builder.Build();

Console.WriteLine("Generate data to database? Press any key to continue");
Console.ReadKey();
await app.Services.GetRequiredService<DataGenerator>().GenerateOrganizations();