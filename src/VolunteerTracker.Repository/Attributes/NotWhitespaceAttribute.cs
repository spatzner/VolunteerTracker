using System.ComponentModel.DataAnnotations;

namespace VolunteerTracker.Repository.Attributes;

public class NotWhitespaceAttribute : RegularExpressionAttribute
{
    public NotWhitespaceAttribute() : base(@"\S+")
    {
        ErrorMessage = "Cannot be empty";
    }
}