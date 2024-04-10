using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VolunteerTracker.Common.DataAnnotations;
using VolunteerTracker.Repository.Attributes;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace VolunteerTracker.Repository.Entities;

[Table("Persons", Schema = "Entities")]
public class Person
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [NotWhitespace]
    [MaxLength(10)]
    public string? Title { get; set; }

    [Required]
    [NotWhitespace]
    [MaxLength(50)]
    public string FirstName { get; set; }

    [NotWhitespace]
    [MaxLength(50)]
    public string? MiddleName { get; set; }

    [Required]
    [NotWhitespace]
    [MaxLength(50)]
    public string LastName { get; set; }

    [NotWhitespace]
    [MaxLength(10)]
    public string? Suffix { get; set; }

    [NotWhitespace]
    [MaxLength(1000)]
    public string? Notes { get; set; }

    [ValidateComplexType]
    public virtual Address? Address { get; set; }

    [ValidateComplexType]
    [ExactlyOneMember<Phone>(nameof(Phone.IsPrimary), true, AllowEmpty = true)]
    public virtual ICollection<Phone> Phones { get; set; }

    [ValidateComplexType]
    [ExactlyOneMember<Email>(nameof(Email.IsPrimary), true, AllowEmpty = true)]
    public virtual ICollection<Email> Emails { get; set; }
    
    public override string ToString() => $"{LastName}, {FirstName}";

    public static Person Create()
    {
        return new Person
        {
            FirstName = string.Empty,
            LastName = string.Empty,
            Address = new Address(),
            Phones = new List<Phone>(),
            Emails = new List<Email>()
        };
    }
}