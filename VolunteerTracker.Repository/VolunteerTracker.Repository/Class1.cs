using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable IDE0079

namespace VolunteerTracker.Repository
{
    public class VolunteerContext(DbContextOptions<VolunteerContext> options) : DbContext(options)
    {
        public DbSet<Donation> Donations { get; set; }
        public DbSet<Entity> Entities { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<DonationSchedule> DonationSchedules { get; set; }
        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<ListValue> ListValues { get; set; }
        public DbSet<ListType> ListTypes { get; set; }
    }

    public class Donation
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        [Range(typeof(decimal),"0.01" ,"1,000,000,000,000")]
        public decimal Amount { get; set; }
        
        [Required]
        [ForeignKey("EntityId")]
        public Entity Entity { get; set; }
        
        [ForeignKey("CampaignId")]
        public Campaign? Campaign { get; set; }
        
        [MaxLength(1000)]
        public string Notes { get; set; }
    }

    public class Entity
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string EntityType { get; set; }

        [MaxLength(1000)]
        public string Notes { get; set; }


        [ForeignKey("ContactId")]
        public required Contact Contact { get; set; }


        public virtual ICollection<Donation> Donations { get; set; }
        public virtual ICollection<Campaign> Campaign { get; set; }
    }

    public class Campaign
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        
        [ForeignKey("EntityId")]
        public required Entity Entity { get; set; }

        [MaxLength(1000)]
        public string Notes { get; set; }
    }
    public enum ScheduleType
    {
        Weekly,
        BiWeekly,
        SemiMonthly,
        Monthly,
        Quarterly,
        Yearly
    }

    public class DonationSchedule
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public ScheduleType Type { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
        public Entity Entity { get; set; } = null!;

        [MaxLength(1000)]
        public string Notes { get; set; }
    }

    public class Contact
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(10)]
        public  string Title { get; set; }
        [MaxLength(50)]
        [RegularExpression(@"\W+", ErrorMessage = "Cannot be empty")]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string? MiddleName { get; set; }
        [MaxLength(50)]
        [RegularExpression(@"\W+", ErrorMessage = "Cannot be empty")]
        public string LastName { get; set; }
        [MaxLength(10)]
        public string? Suffix { get; set; }

        [MaxLength(1000)]
        public string Notes { get; set; }

    }

    public class Address
    {

        [Required]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        [RegularExpression(@"\W+", ErrorMessage = "Cannot be empty")]
        public string Line1 { get; set; }

        [MaxLength(50)]
        [RegularExpression(@"\W+", ErrorMessage = "Cannot be empty")]
        public string? Line2 { get; set; }

        [Required]
        [MaxLength(50)]
        [RegularExpression(@"\W+", ErrorMessage = "Cannot be empty")]
        public  string City { get; set; }

        [Required]
        [MaxLength(50)]
        [RegularExpression(@"\W+", ErrorMessage = "Cannot be empty")]
        public string State { get; set; }
        [Required]
        [MaxLength(10)]
        [RegularExpression(@"\W+", ErrorMessage = "Cannot be empty")]
        public string Zip { get; set; }
    }

    public class ListValue
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Value { get; set; }

        [ForeignKey("ListTypeId")] public ListType Type { get; set; }
    }

    public class ListType
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        [RegularExpression(@"\W+", ErrorMessage = "Cannot be empty")]
        public string Name{ get; set; } = string.Empty;
        public virtual ICollection<ListValue> ListValues { get; set; }
    }
}