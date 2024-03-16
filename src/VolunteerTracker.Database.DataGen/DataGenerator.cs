using System.ComponentModel.DataAnnotations;
using Bogus;
using VolunteerTracker.Repository;
using VolunteerTracker.Repository.Entities;
using Person = VolunteerTracker.Repository.Entities.Person;
using ValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;

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
            Random random = new Random();
            List<Person> persons = [.. new PersonFaker().Generate(100)];
            List<Address> addresses = [.. new AddressFaker().Generate(97)];

            _volunteerContext.Persons.AddRange(persons);
            await _volunteerContext.SaveChangesAsync();

            foreach (Address address in addresses)
                address.PersonId = persons[random.Next(0, persons.Count)].Id;
            

            _volunteerContext.Addresses.AddRange(addresses);

            await _volunteerContext.SaveChangesAsync();
        }

        private sealed class PersonFaker : Faker<Person>
        {
            public PersonFaker()
            {
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
                RuleFor(p => p.Suffix, f => f.Name.Suffix().OrNull(f, .05f));
                RuleFor(p => p.Notes, f => f.Lorem.Sentence().OrNull(f, .8f));
            }
        }

        private sealed class AddressFaker : Faker<Address>
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
    }
}