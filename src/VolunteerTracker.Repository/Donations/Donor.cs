using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VolunteerTracker.Repository.Entities;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace VolunteerTracker.Repository.Donations;

[Table("Donors", Schema = "Donations")]
public class Donor
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public Guid? PersonId { get; set; }

    [ForeignKey("PersonId")]
    public Person? Person { get; set; }

    public Guid? OrganizationId { get; set; }

    [ForeignKey("OrganizationId")]
    public Organization? Organization { get; set; }

    public virtual ICollection<Donation> Donations { get; set; }
    public virtual ICollection<Campaign> Campaigns { get; set; }
}