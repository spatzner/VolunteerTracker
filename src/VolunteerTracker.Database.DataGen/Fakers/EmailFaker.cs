using Bogus;
using VolunteerTracker.Repository.Entities;
using Person = VolunteerTracker.Repository.Entities.Person;

namespace VolunteerTracker.Database.DataGen.Fakers;

public sealed class EmailFaker : Faker<Email>
{
    public static readonly string Primary = "Primary";
    public static readonly string NonPrimary = "NonPrimary";

    readonly Random _random = new();

    public EmailFaker(Person person)
    {

        RuleSet(Primary,
            set =>
            {
                set.RuleFor(p => p.IsPrimary, _ => true);
                RuleFor(e => e.Address, f => f.Internet.Email(person.FirstName, person.LastName));
                RuleFor(p => p.Type,
                    _ =>
                    {
                        int rand = _random.Next(0, 100);

                        return rand switch
                        {
                            < 70 => "Home",
                            _ => "Work",
                        };
                    });
            });
        RuleSet(NonPrimary,
            set =>
            {
                set.RuleFor(p => p.IsPrimary, _ => false);
                RuleFor(e => e.Address, f => f.Internet.Email());
                RuleFor(p => p.Type,
                    _ =>
                    {
                        int rand = _random.Next(0, 100);

                        return rand switch
                        {
                            < 70 => "Home",
                            _ => "Work",
                        };
                    });
            });
    }
}