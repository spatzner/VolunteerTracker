using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
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

    public EditContext? EditContext { get; set; }

    private Person _person = null!;
    
    private static readonly Person PlaceholderPerson = new();

    protected override void OnParametersSet()
    {
        LoadPerson();
        base.OnParametersSet();
    }

    private void LoadPerson()
    {
        _person = PlaceholderPerson;
        _person = PersonGuid.HasValue
            ? Context.Persons.Include(x => x.Emails).Include(x => x.Phones).Include(x => x.Address).FirstOrDefault(p => p.Id == PersonGuid) ?? Person.Create()
            : Person.Create();
        EditContext = new EditContext(_person);
    }

    private async Task Save()
    {
        if (!PersonGuid.HasValue)
            Context.Persons.Add(_person);

        await Context.SaveChangesAsync();
        Close();
    }

    private void Close()
    {
        OnClose.InvokeAsync();
    }

    private async Task OnValidSubmit(EditContext arg)
    {
            await Save();

    }
}