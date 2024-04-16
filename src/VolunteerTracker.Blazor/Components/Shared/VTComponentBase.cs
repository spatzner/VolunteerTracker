using Microsoft.AspNetCore.Components;

namespace VolunteerTracker.Blazor.Components.Shared
{
    public abstract class VTComponentBase : ComponentBase
    {
        [Parameter]
        public string Class { get; set; } = string.Empty;

        [Parameter]
        public string? Id { get; set; }

        [Parameter]
        public string Label { get; set; } = string.Empty;

        protected override void OnInitialized()
        {
            Id ??= Guid.NewGuid().ToString().Replace("-", "");
        }
    }
} 
