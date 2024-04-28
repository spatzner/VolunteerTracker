using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VolunteerTracker.Repository.Attributes;

namespace VolunteerTracker.Repository.Entities;

[Table("Phones", Schema = "Entities")]
public class Phone
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required]
    [AllowedValues("Home", "Work", "Mobile", "Fax")]
    public string Type { get; set; }


    [Required]
    public bool IsPrimary { get; set; }

    [Required]
    [Phone]
    [NotWhitespace]
    [MaxLength(25)]
    public string Number { get; set; }

    [ForeignKey("PersonId")]
    public Guid? PersonId { get; set; }
    
    public virtual Person? Person { get; set; }

    [ForeignKey("OrganizationId")]
    public Guid? OrganizationId { get; set; }

    public virtual Organization? Organization{ get; set; }

    public override string ToString() => $"{Type}: {Number}";
}