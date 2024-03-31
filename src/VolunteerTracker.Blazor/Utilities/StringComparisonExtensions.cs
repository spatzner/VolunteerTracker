namespace VolunteerTracker.Blazor.Utilities;

public static class StringComparisonExtensions
{
    public static bool IsIgnoreCase(this StringComparison comparison)
    {
        return comparison switch
        {
            StringComparison.CurrentCultureIgnoreCase => true,
            StringComparison.InvariantCultureIgnoreCase => true,
            StringComparison.OrdinalIgnoreCase => true,
            _ => false
        };
    }
}