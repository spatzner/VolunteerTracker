using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VolunteerTracker.Repository.Attributes;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace VolunteerTracker.Repository.Entities;

[Table("Addresses", Schema = "Entity")]
public class Address
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(50)]
    [NotWhitespace]
    public string Line1 { get; set; }

    [MaxLength(50)]
    [NotWhitespace]
    public string? Line2 { get; set; }

    [Required]
    [MaxLength(50)]
    [NotWhitespace]
    public string City { get; set; }

    [Required]
    [MaxLength(50)]
    [NotWhitespace]
    public string State { get; set; }

    [Required]
    [MaxLength(10)]
    [NotWhitespace]
    public string Zip { get; set; }

    public Guid? PersonId { get; set; }
    public Person? Person { get; set; }

    public Guid? OrganizationId { get; set; }
    public Organization? Organization { get; set; }
}