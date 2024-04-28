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

    public virtual Phone? MainPhone { get; set; }

    [Required]
    public virtual Address Address { get; set; }

    public Guid? ContactId { get; set; }

    [ForeignKey("ContactId")]
    public virtual Person? Contact { get; set; }

    [NotWhitespace]
    [MaxLength(1000)]
    public string? Notes { get; set; }


}