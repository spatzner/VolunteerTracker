using VolunteerTracker.Repository.Entities;

namespace VolunteerTracker.Blazor.Models;

public class IndividualGridModel(Individual individual)
{
    public Guid Id => individual.Id;
    public string FirstName => individual.FirstName;
    public string LastName { get; set; } = individual.LastName;
    public string Location => individual.Address?.ToString() ?? string.Empty;
    public string? Phone => individual.Phones.FirstOrDefault(x => x.IsPrimary)?.Number;
    public string? Email => individual.Emails.FirstOrDefault(x => x.IsPrimary)?.Address;
    
    public Individual Individual => individual;
}