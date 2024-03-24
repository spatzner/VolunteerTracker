using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace VolunteerTracker.Repository.Entities;

[Table("Emails", Schema = "Entities")]
public class Email
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required]
    [AllowedValues("Home", "Work")]
    public string Type { get; set; }

    [Required]
    public bool IsPrimary { get; set; }

    [Required]
    [EmailAddress]
    public string Address { get; set; }

    [ForeignKey("PersonId")]
    public Guid PersonId { get; set; }

    public virtual Person Person { get; set; }
}