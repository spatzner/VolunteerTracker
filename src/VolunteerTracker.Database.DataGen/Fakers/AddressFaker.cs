using Bogus;
using VolunteerTracker.Repository.Entities;

namespace VolunteerTracker.Database.DataGen.Fakers;

public sealed class AddressFaker : Faker<Address>
{
    public AddressFaker()
    {
        RuleFor(a => a.Line1, f => f.Address.StreetAddress());
        RuleFor(a => a.Line2, f => f.Address.SecondaryAddress().OrNull(f, 0.2f));
        RuleFor(a => a.City, f => f.Address.City());
        RuleFor(a => a.State, f => f.Address.State());
        RuleFor(a => a.Zip, f => f.Address.ZipCode());
    }
}