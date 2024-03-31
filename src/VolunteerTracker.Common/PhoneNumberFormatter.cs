using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolunteerTracker.Common
{
    public interface IStringFormatter
    {
        string Format(string? value);
    }

    public class PhoneNumberFormatter : IStringFormatter
    {

        public string Format(string? value)
        {
            if(string.IsNullOrEmpty(value))
                return string.Empty;
            
            var cleanedParts = GeneratedRegex.NonPhoneCharacter().Replace(value, string.Empty).Split("x");
            string phone = cleanedParts[0];
            string extension = cleanedParts.Length > 1 ? $" x{cleanedParts[1]}" : string.Empty;

            string? result;
            switch (phone.Length)
            {
                case 10:
                    result = GeneratedRegex.NationalPhoneFormatGrouping().Replace(phone, "$1-$2-$3");
                    break;
                case 11 when phone.First() == '1':
                    result = GeneratedRegex.NationalPlusOnePhoneFormatGrouping().Replace(phone, "$1-$2-$3-$4");
                    break;
                case 13:
                    result = GeneratedRegex.InternationalPhoneFormatGrouping().Replace(phone, "$1 ($2) $3 $4-$5");
                    break;
                default:
                    return value;
            }

            if (extension != string.Empty)
                result += $" {extension}";

            return result;
        }

    }
}
