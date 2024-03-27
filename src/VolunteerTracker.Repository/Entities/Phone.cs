using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
    [MaxLength(25)]
    public string Number { get; set; }

    [ForeignKey("PersonId")]
    [Required]
    public Guid PersonId { get; set; }
    
    public virtual Person Person { get; set; }

    public override string ToString() => $"{Type}: {Number}";
}