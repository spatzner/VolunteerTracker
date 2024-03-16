using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using VolunteerTracker.Blazor.Data;
using VolunteerTracker.Blazor.Settings;

namespace VolunteerTracker.Blazor.Services;

public class EmailSender(IOptions<SmtpSettings> smtpSettingsOptions, ILogger<EmailSender> logger)
    : IEmailSender, IEmailSender<ApplicationUser>
{
    private readonly SmtpSettings _smtpSettings = smtpSettingsOptions.Value;

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        try
        {
            using var message = new MailMessage();
            message.From = new MailAddress(_smtpSettings.SenderEmail);
            message.To.Add(new MailAddress(email));
            message.To.Add(new MailAddress(email));
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = htmlMessage;


            using var smtp = new SmtpClient();
            smtp.Port = _smtpSettings.Port;
            smtp.Host = _smtpSettings.Host;
            smtp.EnableSsl = _smtpSettings.UseSsl;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(_smtpSettings.SenderEmail, _smtpSettings.Password);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

            await smtp.SendMailAsync(message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to send an email");
        }
    }

    public async Task SendConfirmationLinkAsync(ApplicationUser user, string email, string confirmationLink)
    {
       await SendEmailAsync(email, "Confirm your email", $"Please confirm your account by <a href='{confirmationLink}'>clicking here</a>.");
    }

    public async Task SendPasswordResetLinkAsync(ApplicationUser user, string email, string resetLink)
    {
        await SendEmailAsync(email, "Reset your password", $"Please reset your password by <a href='{resetLink}'>clicking here</a>.");
    }

    public async Task SendPasswordResetCodeAsync(ApplicationUser user, string email, string resetCode)
    {
        await SendEmailAsync(email, "Reset your password", $"Please reset your password using the following code: {resetCode}");
      
    }
}