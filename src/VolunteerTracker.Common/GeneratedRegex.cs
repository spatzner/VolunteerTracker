using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace VolunteerTracker.Common
{
    public static partial class GeneratedRegex
    {
        [GeneratedRegex(@"[^\dx]")]
        public static partial Regex NonPhoneCharacter();

        [GeneratedRegex(@"(\d{1})(\d{2})(\d{3})(\d{3})(\d{4})")]
        public static partial Regex InternationalPhoneFormatGrouping();

        [GeneratedRegex(@"(\d{3})(\d{3})(\d{4})")]
        public static partial Regex NationalPhoneFormatGrouping();

        [GeneratedRegex(@"(1)(\d{3})(\d{3})(\d{4})")]
        public static partial Regex NationalPlusOnePhoneFormatGrouping();
    }
}
