using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VolunteerTracker.Repository.Attributes;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace VolunteerTracker.Repository.Programs;

[Table("Programs", Schema = "Program")]
public class NonProfitProgram
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(50)]
    [NotWhitespace]
    public string Name { get; set; }

    [MaxLength(1000)]
    [NotWhitespace]
    public string Notes { get; set; }
}