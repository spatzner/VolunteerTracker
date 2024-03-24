using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using VolunteerTracker.Repository.Dbo;
using VolunteerTracker.Repository.Donations;
using VolunteerTracker.Repository.Entities;
using VolunteerTracker.Repository.Programs;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace VolunteerTracker.Repository;

public class VolunteerContext(DbContextOptions<VolunteerContext> options) : DbContext(options)
{
    // Entities
    public DbSet<Person> Persons { get; set; }
    public DbSet<Organization> Organizations { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Phone> Phones { get; set; }
    public DbSet<Email> Emails { get; set; }
    

    // Donations
    public DbSet<Donation> Donations { get; set; }
    public DbSet<DonationSchedule> DonationSchedules { get; set; }
    public DbSet<Campaign> Campaigns { get; set; }
    public DbSet<Donor> Donors { get; set; }

    //Programs
    public DbSet<NonProfitProgram> Programs { get; set; }

    //dbo
    public DbSet<ListValue> ListValues { get; set; }
    public DbSet<ListType> ListTypes { get; set; }
}