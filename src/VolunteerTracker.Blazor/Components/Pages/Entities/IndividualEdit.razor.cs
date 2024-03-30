using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using VolunteerTracker.Repository;
using VolunteerTracker.Repository.Entities;

namespace VolunteerTracker.Blazor.Components.Pages.Entities;

public partial class IndividualEdit
{
    [Parameter]
    public required Guid? PersonGuid { get; set; }

    [Parameter]
    public EventCallback OnClose { get; set; }

    [Inject]
    public required VolunteerContext Context { get; set; }

    private Person _person = new();
    protected override void OnInitialized()
    {
        _person = PersonGuid.HasValue
            ? Context.Persons.Include(x => x.Emails).Include(x => x.Phones).Include(x => x.Address).FirstOrDefault(p => p.Id == PersonGuid) ?? Person.Create()
            : Person.Create();

        base.OnInitialized();
    }

    private void Save()
    {
        if (!PersonGuid.HasValue)
            Context.Persons.Add(_person);

        Context.SaveChanges();
        Close();
    }

    private void Close()
    {
        OnClose.InvokeAsync();
    }
}