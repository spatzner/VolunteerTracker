using VolunteerTracker.Repository.Entities;

namespace VolunteerTracker.Blazor.Utilities
{
    public static class IsEmptyExtensions
    {
        public static bool IsEmpty(this Address address)
        {
            return  string.IsNullOrWhiteSpace(address.Line1) && string.IsNullOrWhiteSpace(address.Line2) && string.IsNullOrWhiteSpace(address.City) && string.IsNullOrWhiteSpace(address.State) && string.IsNullOrWhiteSpace(address.Zip);
        }

        public static bool IsEmpty(this Phone phone)
        {
            return string.IsNullOrWhiteSpace(phone.Number);
        }

        public static bool IsEmpty(this Email phone)
        {
            return string.IsNullOrWhiteSpace(phone.Address);
        }
    }
}
