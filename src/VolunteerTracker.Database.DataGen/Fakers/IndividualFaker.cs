using Bogus;
using VolunteerTracker.Repository.Entities;

namespace VolunteerTracker.Database.DataGen.Fakers;

public sealed class IndividualFaker : Faker<Individual>
{
    public IndividualFaker()
    {
        AddressFaker addressFaker = new AddressFaker();
        PhoneFaker phoneFaker = new PhoneFaker();

        RuleFor(p => p.Title, f => f.Name.Prefix().OrNull(f, .8f));
        RuleFor(p => p.FirstName, f => f.Name.FirstName());
        RuleFor(p => p.MiddleName,
            f =>
            {
                var middle = f.Name.FirstName().OrNull(f, .7f);
                if (middle is not null)
                    middle = Random.Shared.Next(0, 5) % 5 == 0 ? middle : middle[..1];
                return middle;
            });
        RuleFor(p => p.LastName, f => f.Name.LastName());
        RuleFor(p => p.Suffix, f => f.Name.Suffix().OrNull(f, .95f));
        RuleFor(p => p.Notes, f => f.Lorem.Sentence().OrNull(f, .8f));
        RuleFor(p => p.Address, _ => addressFaker.Generate());
        RuleFor(p => p.Phones,
            _ =>
            {
                ICollection<Phone> phones = phoneFaker.Generate(1, PhoneFaker.Primary);

                if (Random.Shared.Next(0, 100) < 50)
                    phones.Add(phoneFaker.Generate(PhoneFaker.NonPrimary));

                return phones;
            });
        RuleFor(p => p.Emails,
            (_, p) =>
            {
                EmailFaker emailFaker = new EmailFaker(p);
                ICollection<Email> emails = emailFaker.Generate(1, PhoneFaker.Primary);

                if (Random.Shared.Next(0, 100) < 10)
                    emails.Add(emailFaker.Generate(PhoneFaker.NonPrimary));

                return emails;
            });
    }
}