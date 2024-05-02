using VolunteerTracker.Repository.Entities;

namespace VolunteerTracker.Blazor.Utilities
{
    public static class Entity
    {
        public static bool IsEmpty(Address? address)
        {
            if(address == null)
                return false;
            
            return string.IsNullOrWhiteSpace(address.Line1)
             && string.IsNullOrWhiteSpace(address.Line2)
             && string.IsNullOrWhiteSpace(address.City)
             && string.IsNullOrWhiteSpace(address.State)
             && string.IsNullOrWhiteSpace(address.Zip);
        }

        public static bool IsEmpty(Person? person)
        {
            if(person == null)
                return false;
            
            return string.IsNullOrWhiteSpace(person.FirstName) && string.IsNullOrWhiteSpace(person.LastName);
        }

        public static bool IsEmpty(Phone? phone)
        {
            if(phone == null)
                return false;
            
            return string.IsNullOrWhiteSpace(phone.Number);
        }

        public static bool IsEmpty(Email? email)
        {
            if(email == null)
                return false;
            
            return string.IsNullOrWhiteSpace(email.Address);
        }
    }
}