using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace VolunteerTracker.Repository.Attributes;

public class NotWhitespaceAttribute : RegularExpressionAttribute
{
    public NotWhitespaceAttribute() : base(@".*\S.*")
    {
        ErrorMessage = "Cannot be empty";
        
    }

    public override bool IsValid(object? value)
    {
        var isValid = base.IsValid(value);
        return isValid;
    }
}