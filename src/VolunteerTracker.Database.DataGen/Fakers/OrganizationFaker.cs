using Bogus;
using VolunteerTracker.Repository.Entities;

namespace VolunteerTracker.Database.DataGen.Fakers;

public sealed class OrganizationFaker : Faker<Organization>
{
    public OrganizationFaker()
    {
        AddressFaker addressFaker = new();
        PhoneFaker phoneFaker = new();

        RuleFor(o => o.Name, f => f.Company.CompanyName());
        RuleFor(o => o.Address, f => addressFaker.Generate().OrNull(f, .05f));
        RuleFor(o => o.MainPhone, f => phoneFaker.Generate(PhoneFaker.Primary).OrNull(f, .1f));
        RuleFor(o => o.Contact, f => new PersonFaker().Generate().OrNull(f, .1f));
        RuleFor(o => o.Notes, f => f.Lorem.Sentences(Random.Shared.Next(1, 3), "\r\n").OrNull(f));
    }
}