using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolunteerTracker.Blazor.PlaywrightTests
{
    public static class BlazorEndpoints
    {

        public static string Individuals => _individualUri;
        public static string Organizations => _organizationUri;
        
        private static string _individualUri = null!;
        private static string _organizationUri = null!;
        
        public static void SetSettings(BlazorEndpointsSettings settings)
        {;
            var baseUri = new Uri(settings.Url);
            _individualUri = new Uri(baseUri, settings.Individuals).ToString();
            _organizationUri = new Uri(baseUri, settings.Organizations).ToString();
        }
    }
    
    public class BlazorEndpointsSettings
    {
        public string Url { get; set; } = null!;
        public string Individuals { get; set; } = null!;
        public string Organizations { get; set; } = null!;
    }
}
