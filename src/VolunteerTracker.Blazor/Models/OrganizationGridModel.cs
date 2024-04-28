using VolunteerTracker.Repository.Entities;

namespace VolunteerTracker.Blazor.Models;

public class OrganizationGridModel(Organization organization)
{
    public Guid Id => organization.Id;
    public string Name => organization.Name;
    public string Location => organization.Address?.ToString() ?? string.Empty;
    public string? MainPhone => organization.MainPhone?.Number;
    public string? Contact => organization.Contact?.ToString();
    
    public Organization Organization => organization;
}