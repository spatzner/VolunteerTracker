using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VolunteerTracker.Repository.Attributes;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace VolunteerTracker.Repository.Entities;

[Table("Addresses", Schema = "Entities")]
public class Address
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required]
    [NotWhitespace]
    [MaxLength(50)]
    public string Line1 { get; set; }

    [NotWhitespace]
    [MaxLength(50)]
    public string? Line2 { get; set; }

    [Required]
    [NotWhitespace]
    [MaxLength(50)]
    public string City { get; set; }

    [Required]
    [NotWhitespace]
    [MaxLength(50)]
    public string State { get; set; }

    [Required]
    [NotWhitespace]
    [MaxLength(10)]
    public string Zip { get; set; }

    [ForeignKey("IndividualId")]
    public virtual Individual? Individual { get; set; }

    [ForeignKey("OrganizationId")]
    public virtual Organization? Organization { get; set; }

    public override string ToString()
    {
        StringBuilder sb = new();
        sb.Append(Line1);
        if (!string.IsNullOrWhiteSpace(Line2))
        {
            sb.Append($", {Line2}");
        }

        sb.Append($", {City}, {State} {Zip}");

        return sb.ToString();
    }
}