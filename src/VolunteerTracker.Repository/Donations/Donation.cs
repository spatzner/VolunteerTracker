using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VolunteerTracker.Repository.Attributes;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace VolunteerTracker.Repository.Donations;

[Table("Donations", Schema = "Donations")]
public class Donation
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required]
    [Range(typeof(decimal), "0.01", "1,000,000,000,000", ErrorMessage = "Must be at least one cent")]
    public decimal Amount { get; set; }

    [Required]
    public Guid DonorId { get; set; }

    [ForeignKey("DonorId")]
    public Donor Donor { get; set; }

    public Guid? CampaignId { get; set; }

    [ForeignKey("CampaignId")]
    public Campaign? Campaign { get; set; }

    [MaxLength(1000)]
    [NotWhitespace]
    public string Notes { get; set; }
}