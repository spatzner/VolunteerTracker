﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VolunteerTracker.Blazor.Settings;
using VolunteerTracker.Repository;
using VolunteerTracker.Repository.Entities;

namespace VolunteerTracker.Blazor.PlaywrightTests
{
    [TestClass]
    public class GlobalBeforeAndAfter
    {
        private static readonly IDbContextFactory<VolunteerContext> ContextFactory;

        static GlobalBeforeAndAfter()
        {
            var builder = Host
               .CreateDefaultBuilder([])
               .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddJsonFile("appsettings.json", optional: false);
                    config.AddUserSecrets<GlobalBeforeAndAfter>();
                })
               .ConfigureServices((hostContext, services) =>
                {

                    var databaseSettings = new VolunteerDatabaseSettings();
                    hostContext.Configuration.GetSection("VolunteerDatabaseSettings").Bind(databaseSettings);

                    services.AddDbContextFactory<VolunteerContext>(options =>
                    {
                        options.UseNpgsql(databaseSettings.ConnectionString);
                    });
                });


            var app = builder.Build();

            ContextFactory = app.Services.GetRequiredService<IDbContextFactory<VolunteerContext>>();
        }

        [AssemblyInitialize()]
        public static void MyTestInitialize(TestContext testContext)
        {
            using var context = ContextFactory.CreateDbContext();

            context.Persons.RemoveRange(context.Persons.Where(p => p.FirstName == "Cheddar"));
            context.Persons.RemoveRange(context.Persons.Where(p => p.LastName == "VanCheddar"));
            context.Persons.RemoveRange(context.Persons.Where(p => p.LastName == "VonCheddar"));

            context.Persons.Add(new Person
            {
                Title = "Miss",
                FirstName = "Janice",
                MiddleName = "R",
                LastName = "VonCheddar",
                Suffix = "MRO",
                Address = new Address
                {
                    Line1 = "55100 E Elm St",
                    Line2 = "Apt 2",
                    City = "Springfield",
                    State = "IL",
                    Zip = "62701",
                },
                Emails =
                [
                    new Email { Type = "Home", Address = "JaniceTheManice@raikko.org", IsPrimary = true },
                    new Email { Type = "Work", Address = "JVonCheddar@raikko.org", IsPrimary = false }
                ],
                Phones =
                [
                    new Phone { Type = "Mobile", Number = "231523699", IsPrimary = true },
                    new Phone { Type = "Work", Number = "2315236621", IsPrimary = false }
                ],
                Notes = "Janice is a very nice person."
            });

            context.SaveChanges();
        }

        [AssemblyCleanup]
        public static void TearDown()
        {
            using var context = ContextFactory.CreateDbContext();

            context.Persons.RemoveRange(context.Persons.Where(p => p.FirstName == "Cheddar"));
            context.Persons.RemoveRange(context.Persons.Where(p => p.LastName == "VanCheddar"));
            context.Persons.RemoveRange(context.Persons.Where(p => p.LastName == "VanCheddar"));

            context.SaveChanges();
        }
    }
}