using System.ComponentModel.DataAnnotations;

namespace VolunteerTracker.Repository.Entities;

#pragma warning disable CS8618
public class Individual : Person
{
    
    [ValidateComplexType]
    public virtual Address? Address { get; set; }
}