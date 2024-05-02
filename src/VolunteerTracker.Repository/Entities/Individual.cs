using System.ComponentModel.DataAnnotations;

namespace VolunteerTracker.Repository.Entities;

#pragma warning disable CS8618
public class Individual : Person
{
    [ValidateComplexType]
    public virtual Address? Address { get; set; }

    public static Individual Create()
    {
        return new Individual { Address = new Address(), Phones = [new Phone { IsPrimary = true }], Emails = [new Email { IsPrimary = true }] };
    }
}