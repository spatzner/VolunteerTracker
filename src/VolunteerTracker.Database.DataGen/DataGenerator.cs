using VolunteerTracker.Database.DataGen.Fakers;
using VolunteerTracker.Repository;
using VolunteerTracker.Repository.Entities;

namespace VolunteerTracker.Database.DataGen
{
    public class DataGenerator(VolunteerContext volunteerContext)
    {
        public async Task GenerateDataToDatabase()
        {
            await GenerateIndividuals();
            await GenerateOrganizations();
        }

        public async Task GenerateIndividuals()
        {
            List<Individual> individuals = [.. new IndividualFaker().Generate(1000)];
            volunteerContext.Individuals.AddRange(individuals);

            await volunteerContext.SaveChangesAsync();
        }

        public async Task GenerateOrganizations()
        {
            List<Organization> organizations = [.. new OrganizationFaker().Generate(1000)];
            volunteerContext.Organizations.AddRange(organizations);

            await volunteerContext.SaveChangesAsync();
        }
    }
}