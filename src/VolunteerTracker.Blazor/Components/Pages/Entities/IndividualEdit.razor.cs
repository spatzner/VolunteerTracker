using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using VolunteerTracker.Repository;
using VolunteerTracker.Repository.Entities;

namespace VolunteerTracker.Blazor.Components.Pages.Entities;

public partial class IndividualEdit : IDisposable, IAsyncDisposable
{
    [Parameter]
    public required Guid? PersonGuid { get; set; }

    [Parameter]
    public EventCallback OnClose { get; set; }

    [Inject]
    public required IDbContextFactory<VolunteerContext> ContextFactory { get; set; }

    private VolunteerContext? _context;
    public EditContext? EditContext { get; set; }
    private Person _person = null!;
    private static readonly Person PlaceholderPerson = new();

    protected override async Task OnParametersSetAsync()
    {
        await LoadPersonAsync();
        await base.OnParametersSetAsync();
    }
    
    private async Task LoadPersonAsync()
    {
        if(_context != null)
            await _context.DisposeAsync();
        _context = await ContextFactory.CreateDbContextAsync();
        _person = PlaceholderPerson;
        _person = PersonGuid.HasValue
            ? await _context.Persons.Include(x => x.Emails).Include(x => x.Phones).Include(x => x.Address).FirstOrDefaultAsync(p => p.Id == PersonGuid) ?? Person.Create()
            : Person.Create();

        EditContext = new EditContext(_person);
    }

    private async Task Save()
    {
        if (!PersonGuid.HasValue)
            _context!.Persons.Add(_person);

        await _context!.SaveChangesAsync();
        await _context.DisposeAsync();
        EditContext = null;
        await OnClose.InvokeAsync();
    }

    private async Task Close()
    {
        if(_context != null)
            await _context.DisposeAsync();
        EditContext = null;
        await OnClose.InvokeAsync();
    }

    private async Task OnValidSubmit(EditContext arg)
    {
        await Save();
    }

    public void Dispose()
    {
        _context?.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        if (_context != null)
            await _context.DisposeAsync();
    }
}