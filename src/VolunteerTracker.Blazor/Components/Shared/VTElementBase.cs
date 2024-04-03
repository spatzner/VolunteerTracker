using Microsoft.AspNetCore.Components;

namespace VolunteerTracker.Blazor.Components.Shared
{
    public abstract class VTElementBase : VTComponentBase
    {
        [Parameter]
        public string ContainerClass { get; set; } = string.Empty;
    }
}
