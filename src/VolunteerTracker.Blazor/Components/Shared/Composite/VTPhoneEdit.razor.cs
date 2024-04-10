using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using VolunteerTracker.Common.DataAnnotations;
using VolunteerTracker.Repository.Entities;

namespace VolunteerTracker.Blazor.Components.Shared.Composite;

public partial class VTPhoneEdit
{
    [Parameter]
    public required ICollection<Phone> List { get; set; }

    [Parameter]
    public EventCallback<ICollection<Phone>> ListChanged { get; set; }

    [CascadingParameter]
    public EditContext EditContext { get; set; } = null!;

    private readonly List<string> _phoneTypes = typeof(Phone).GetAllowedValues(nameof(Phone.Type));

    private void DeletePhone(Guid phoneId)
    {
        List.Remove(List.First(x => x.Id == phoneId));
        if(List.All(p => !p.IsPrimary) && List.Count != 0)
            List.First().IsPrimary = true;
        ListChanged.InvokeAsync(List);
    }

    private void AddPhone(MouseEventArgs e)
    {
        List.Add(new Phone { Type = _phoneTypes.First(), IsPrimary = !List.Any() });
        ListChanged.InvokeAsync(List);
    }

    private void NumberChanged(Phone phone, string newValue)
    {
        if (phone.Number == newValue)
            return;

        phone.Number = newValue;

        Validate(phone, nameof(phone.Number));

        ListChanged.InvokeAsync(List);
    }

    private void MainSelected(Phone phone)
    {
        phone.IsPrimary = true;

        foreach (Phone p in List.Where(p => p != phone))
        {
            p.IsPrimary = false;
        }

        Validate(phone, nameof(phone.IsPrimary));
        ListChanged.InvokeAsync(List);
    }

    private void TypeChanged(Phone phone, string newValue)
    {
        if (phone.Type == newValue)
            return;

        phone.Type = newValue;
        Validate(phone, nameof(phone.Type));
        ListChanged.InvokeAsync(List);
    }

    private void Validate(Phone phone, string propertyName)
    {
        var index = List.ToList().IndexOf(phone);
        var fullPropertyName = $"{nameof(Person.Phones)}[{index}].{propertyName}";
        var fieldIdentifier = new FieldIdentifier(EditContext.Model, fullPropertyName);

        EditContext.NotifyFieldChanged(fieldIdentifier);
        EditContext.Validate();
    }
}