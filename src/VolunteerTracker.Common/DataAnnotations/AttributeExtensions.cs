using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace VolunteerTracker.Common.DataAnnotations
{
    public static class AttributeExtensions
    {
        public static List<string> GetAllowedValues(this Type type, string propertyName)
        {
            var typeProperty = type.GetProperty(propertyName);
            var attribute =  (AllowedValuesAttribute)typeProperty.GetCustomAttributes(typeof(AllowedValuesAttribute),false).First();
            var values = attribute.Values.Select(x => (string)x!).ToList();
            return values;
        }
    }
}
