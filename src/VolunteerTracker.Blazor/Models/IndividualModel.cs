using VolunteerTracker.Repository.Entities;

namespace VolunteerTracker.Blazor.Models;

public class IndividualModel(Person person)
{
    public Guid Id => person.Id;
    public string FirstName => person.FirstName;
    public string LastName { get; set; } = person.LastName;
    public string Location => person.Address.ToString();
    public string? Phone => person.Phones.FirstOrDefault(x => x.IsPrimary)?.Number;
    public string? Email => person.Emails.FirstOrDefault(x => x.IsPrimary)?.Address;
    
    public Person Person => person;

    public bool ShowDetails { get; set; }
}