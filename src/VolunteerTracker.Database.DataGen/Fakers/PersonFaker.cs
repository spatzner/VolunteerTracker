using VolunteerTracker.Repository.Entities;
using ExtensionsForFakerT = Bogus.ExtensionsForFakerT;

namespace VolunteerTracker.Database.DataGen.Fakers;

public sealed class PersonFaker : Bogus.Faker<Person>
{
    public PersonFaker()
    {
        PhoneFaker phoneFaker = new();

        RuleFor(p => p.Title, f => ExtensionsForFakerT.OrNull(f.Name.Prefix(), f, .8f));
        RuleFor(p => p.FirstName, f => f.Name.FirstName());
        RuleFor(p => p.MiddleName,
            f =>
            {
                var middle = ExtensionsForFakerT.OrNull(f.Name.FirstName(), f, .7f);
                if (middle is not null)
                    middle = Random.Shared.Next(0, 5) % 5 == 0 ? middle : middle[..1];
                return middle;
            });
        RuleFor(p => p.LastName, f => f.Name.LastName());
        RuleFor(p => p.Suffix, f => ExtensionsForFakerT.OrNull(f.Name.Suffix(), f, .95f));
        //RuleFor(p => p.Notes, f => ExtensionsForFakerT.OrNull(f.Lorem.Sentence(), f, .8f));
        RuleFor(p => p.Phones, _ => phoneFaker.Generate(1, PhoneFaker.Primary));
        RuleFor(p => p.Emails,
            (_, p) =>
            {
                EmailFaker emailFaker = new EmailFaker(p);
                return emailFaker.Generate(1, PhoneFaker.Primary);
            });
    }
}