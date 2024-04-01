using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace VolunteerTracker.Common.DataAnnotations;

public class ExactlyOneMemberAttribute<T>(string propertyName, object uniqueValue) : ValidationAttribute
{
    public object UniqueValue { get; } = uniqueValue;
    public PropertyInfo PropertyInfo { get; } = typeof(T).GetProperty(propertyName) ?? 
        throw new ArgumentException("Property not found", propertyName);

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if(value == null)
            return ValidationResult.Success;

        var list = ((IEnumerable)value).Cast<T>();

        return list.Count(p => Equals(PropertyInfo.GetValue(p), UniqueValue)) != 1
            ? new ValidationResult($"Must have exactly one element with {propertyName} = {UniqueValue}")
            : ValidationResult.Success;
    }
}