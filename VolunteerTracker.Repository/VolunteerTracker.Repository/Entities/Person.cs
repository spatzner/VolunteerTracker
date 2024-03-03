using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VolunteerTracker.Repository.Attributes;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace VolunteerTracker.Repository.Entities;

[Table("Persons", Schema = "Entities")]
public class Person
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [NotWhitespace]
    [MaxLength(10)]
    public string? Title { get; set; }

    [MaxLength(50)]
    [NotWhitespace]
    public string FirstName { get; set; }

    [MaxLength(50)]
    [NotWhitespace]
    public string? MiddleName { get; set; }

    [MaxLength(50)]
    [NotWhitespace]
    public string LastName { get; set; }

    [MaxLength(10)]
    [NotWhitespace]
    public string? Suffix { get; set; }

    [MaxLength(1000)]
    [NotWhitespace]
    public string? Notes { get; set; }
}