using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace VolunteerTracker.Common.DataAnnotations;

public class ExactlyOneMemberAttribute<T>(string propertyName, object uniqueValue) : ValidationAttribute
{
    public object UniqueValue { get; } = uniqueValue;
    public bool AllowEmpty { get; set; }
    public PropertyInfo PropertyInfo { get; } = typeof(T).GetProperty(propertyName) ?? 
        throw new ArgumentException("Property not found", propertyName);

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if(value == null)
            return ValidationResult.Success;

        List<T> list = ((IEnumerable)value).Cast<T>().ToList();

        return (list.Count == 0 && AllowEmpty) || list.Count(p => Equals(PropertyInfo.GetValue(p), UniqueValue)) == 1
            ? ValidationResult.Success
            : new ValidationResult($"Must have exactly one element with {propertyName} = {UniqueValue}");

    }
}