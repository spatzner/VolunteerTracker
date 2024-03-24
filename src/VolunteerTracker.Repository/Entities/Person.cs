using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
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

    public virtual Address Address { get; set; }

    [ExactlyOneMember<Phone>(nameof(Phone.IsPrimary), true)]
    public virtual ICollection<Phone> Phones { get; set; }

    [ExactlyOneMember<Email>(nameof(Email.IsPrimary), true)]
    public virtual ICollection<Email> Emails { get; set; }
}