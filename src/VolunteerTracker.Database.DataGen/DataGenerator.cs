using System.ComponentModel.DataAnnotations;
using Bogus;
using VolunteerTracker.Repository;
using VolunteerTracker.Repository.Entities;
using Person = VolunteerTracker.Repository.Entities.Person;
using ValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;

namespace VolunteerTracker.Database.DataGen
{
    public class DataGenerator(VolunteerContext volunteerContext)
    {
        public async Task GenerateDataToDatabase()
        {
            List<Person> persons = GeneratePersons();
            List<Address> addresses = GenerateAddresses();

            volunteerContext.Persons.AddRange(persons
               .Select(person => new { person, validationContext = new ValidationContext(person) })
               .Where(t => Validator.TryValidateObject(t.person, t.validationContext, new List<ValidationResult>()))
               .Select(t => t.person));

            await volunteerContext.SaveChangesAsync();
        }

        private List<Person> GeneratePersons()
        {
            var personGen = new Faker<Person>();

            return personGen
               .Generate(100);
        }

        private List<Address> GenerateAddresses()
        {
            var f = new Faker();

            var addressGen = new Faker<Address>().RuleSet("address",
                set =>
                {
                    Random r = new Random();
                    bool secondaryAddress = r.Next(0, 30) == 1;
                    set.RuleFor(a => a.Line1, f.Address.StreetAddress());
                    set.RuleFor(a => a.Line2, secondaryAddress ? f.Address.SecondaryAddress : null);
                    set.RuleFor(a => a.City, f.Address.City());
                    set.RuleFor(a => a.State, f.Address.State());
                    set.RuleFor(a => a.Zip, f.Address.ZipCode());
                });

            return addressGen.Generate(97);
        }
    }
}