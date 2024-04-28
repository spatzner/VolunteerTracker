using Microsoft.AspNetCore.Components;
using VolunteerTracker.Repository.Entities;

namespace VolunteerTracker.Blazor.Utilities
{
    public class AddressFormatter
    {
        public MarkupString Format(Address? address)
        {
            if (address == null)
                return (MarkupString)string.Empty;

            var address2 = string.IsNullOrWhiteSpace(address.Line2) ? string.Empty : $", {address.Line2}";

            return (MarkupString)$"{address.Line1}{address2}<br />{address.City}, {address.State} {address.Zip}";
        }
    }
}
