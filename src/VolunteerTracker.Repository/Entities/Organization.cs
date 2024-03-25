using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VolunteerTracker.Repository.Attributes;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace VolunteerTracker.Repository.Entities;

[Table("Organizations", Schema = "Entities")]
public class Organization
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }


    [Required]
    [NotWhitespace]
    [MaxLength(50)]
    public string Name { get; set; }

    [ForeignKey("AddressId")]
    [Required]
    public Guid AddressId { get; set; }
    [Required]
    public Address Address { get; set; }

    [Required]
    public Guid ContactId { get; set; }

    [ForeignKey("ContactId")]
    [Required]
    public virtual Person Contact { get; set; }
}