using Bogus;
using VolunteerTracker.Repository.Entities;

namespace VolunteerTracker.Database.DataGen.Fakers;

public sealed class PhoneFaker : Faker<Phone>
{
    public static readonly string Primary = "Primary";
    public static readonly string NonPrimary = "NonPrimary";

    readonly Random _random = new();

    public PhoneFaker()
    {


        RuleSet(Primary,
            set =>
            {
                set.RuleFor(p => p.IsPrimary, _ => true);
                RuleFor(p => p.Number, f => f.Phone.PhoneNumber());
                RuleFor(p => p.Type,
                    _ =>
                    {
                        int rand = _random.Next(0, 100);

                        return rand switch
                        {
                            < 50 => "Mobile",
                            < 70 => "Home",
                            < 99 => "Work",
                            _ => "Fax"
                        };
                    });
            });
        RuleSet(NonPrimary,
            set =>
            {
                set.RuleFor(p => p.IsPrimary, _ => false);
                RuleFor(p => p.Number, f => f.Phone.PhoneNumber());
                RuleFor(p => p.Type,
                    _ =>
                    {
                        int rand = _random.Next(0, 100);

                        return rand switch
                        {
                            < 50 => "Mobile",
                            < 70 => "Home",
                            < 99 => "Work",
                            _ => "Fax"
                        };
                    });
            });
    }
}