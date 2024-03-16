using System.Security;

namespace VolunteerTracker.Blazor.Settings;

public class SmtpSettings
{
    public string Host { get; set; } 
    public int Port { get; set; } = 25;
    public string SenderEmail { get; set; }
    public SecureString? Password { get; set; }
    public bool UseSsl { get; set; }
}