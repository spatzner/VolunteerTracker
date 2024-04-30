using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using VolunteerTracker.Blazor.Utilities;
using VolunteerTracker.Repository;
using VolunteerTracker.Repository.Entities;

namespace VolunteerTracker.Blazor.Components.Entities;

public partial class IndividualEdit : IDisposable, IAsyncDisposable
{
    [Parameter]
    public required Guid? IndividualGuid { get; set; }

    [Parameter]
    public EventCallback OnClose { get; set; }

    [Inject]
    public required IDbContextFactory<VolunteerContext> ContextFactory { get; set; }

    private VolunteerContext? _context;
    public EditContext? EditContext { get; set; }
    private Individual _individual = null!;
    private static readonly Individual IndividualPlaceholder = new();

    protected override async Task OnParametersSetAsync()
    {
        await LoadIndividualAsync();
        await base.OnParametersSetAsync();
    }
    
    private async Task LoadIndividualAsync()
    {
        if(_context != null)
            await _context.DisposeAsync();
        _context = await ContextFactory.CreateDbContextAsync();
        _individual = IndividualPlaceholder;

        _individual = IndividualGuid.HasValue
            ? await _context.Individuals.Include(x => x.Emails).Include(x => x.Phones).Include(x => x.Address).FirstAsync(p => p.Id == IndividualGuid)
            : new Individual { Address = new Address(), Phones = [new Phone { IsPrimary = true }], Emails = [new Email { IsPrimary = true }] };

        EditContext = new EditContext(_individual);
    }

    private async Task OnSubmitAsync(EditContext arg)
    {
        if (_individual.Address != null && _individual.Address.IsEmpty())
            _individual.Address = null;

        var phonesToRemove = _individual.Phones.Where(p => p.IsEmpty()).ToList();
        foreach (Phone phone in phonesToRemove)
        {
            _individual.Phones.Remove(phone);
        }

        var emailsToRemove = _individual.Emails.Where(e => e.IsEmpty()).ToList();
        foreach (Email email in emailsToRemove)
        {
            _individual.Emails.Remove(email);
        }

        if (!arg.Validate())
            return;

        await SaveAsync();
    }

    private async Task SaveAsync()
    {
        if (!IndividualGuid.HasValue)
            _context!.Individuals.Add(_individual);

        await _context!.SaveChangesAsync();
        await _context.DisposeAsync();
        EditContext = null;
        await OnClose.InvokeAsync();
    }

    private async Task CloseAsync()
    {
        if(_context != null)
            await _context.DisposeAsync();
        EditContext = null;
        await OnClose.InvokeAsync();
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