using Bogus;
using VolunteerTracker.Repository;
using VolunteerTracker.Repository.Entities;
using Person = VolunteerTracker.Repository.Entities.Person;

namespace VolunteerTracker.Database.DataGen
{
    public class DataGenerator
    {
        private readonly VolunteerContext _volunteerContext;

        public DataGenerator(VolunteerContext volunteerContext)
        {
            _volunteerContext = volunteerContext;
        }

        public async Task GenerateDataToDatabase()
        {
            List<Person> persons = [.. new PersonFaker().Generate(1000)];
            _volunteerContext.Persons.AddRange(persons);

            await _volunteerContext.SaveChangesAsync();
        }
    }

    public sealed class PersonFaker : Faker<Person>
    {
        public PersonFaker()
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
}