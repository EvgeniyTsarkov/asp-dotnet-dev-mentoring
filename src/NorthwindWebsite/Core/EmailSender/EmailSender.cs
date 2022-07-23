using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using NorthwindWebsite.Core.ApplicationSettings;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace NorthwindWebsite.Core.EmailSender;

public class EmailSender : IEmailSender
{
    private readonly AppSettings _appSettings;

    public EmailSender(
        IOptions<AuthMessageSenderOptions> optionsAccessor,
        AppSettings appSettings)
    {
        Options = optionsAccessor.Value;
        _appSettings = appSettings;
    }

    public AuthMessageSenderOptions Options { get; } //set only via Secret Manager

    public Task SendEmailAsync(string email, string subject, string message) =>
        Execute(Options.SendGridApiKey, subject, message, email);

    private Task Execute(string apiKey, string subject, string message, string email)
    {
        var client = new SendGridClient(apiKey);
        var msg = new SendGridMessage()
        {
            From = new EmailAddress(_appSettings.EmailSenderConfigs.SendersEmail,
                _appSettings.EmailSenderConfigs.SendersName),
            Subject = subject,
            PlainTextContent = message,
            HtmlContent = message
        };
        msg.AddTo(new EmailAddress(email));

        // Disable click tracking.
        // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
        msg.SetClickTracking(enable: false, enableText: false);

        return client.SendEmailAsync(msg);
    }
}
