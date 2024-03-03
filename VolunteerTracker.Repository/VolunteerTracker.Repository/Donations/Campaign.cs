using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VolunteerTracker.Repository.Attributes;
using VolunteerTracker.Repository.Entities;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace VolunteerTracker.Repository.Donations;

[Table("Campaigns", Schema = "Donations")]
public class Campaign
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(50)]
    [NotWhitespace]
    public string Name { get; set; }

    [Required]
    public Guid OrganizationId { get; set; }

    [ForeignKey("OrganizationId")]
    public Organization Organization { get; set; }

    [MaxLength(1000)]
    [NotWhitespace]
    public string Notes { get; set; }
}