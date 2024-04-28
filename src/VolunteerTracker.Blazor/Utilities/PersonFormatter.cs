using Microsoft.AspNetCore.Components;
using System.Text;
using VolunteerTracker.Repository.Entities;

namespace VolunteerTracker.Blazor.Utilities
{
    public class PersonFormatter
    {
        public MarkupString Format(Person? person)
        {
            if (person == null)
                return new MarkupString(string.Empty);

            var sb = new StringBuilder();
            sb.Append($"{person.FirstName} {person.LastName}");

            var phone = person.Phones?.FirstOrDefault(p => p.IsPrimary)?.ToString();
            if (phone != null)
                sb.Append($"<br/>{phone}");

            var email = person.Emails.FirstOrDefault(e => e.IsPrimary)?.ToString();
            if (email != null)
                sb.Append($"<br/>{email}");
            return (MarkupString)sb.ToString();
        }
    }
}
