using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VolunteerTracker.Repository.Attributes;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace VolunteerTracker.Repository.Donations;

[Table("DonationSchedules", Schema = "Donations")]
public class DonationSchedule
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required]
    public ScheduleType Type { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public Guid DonorId { get; set; }

    [ForeignKey("DonorId")]
    public Donor Donor { get; set; }

    [MaxLength(1000)]
    [NotWhitespace]
    public string Notes { get; set; }
}