using System.ComponentModel.DataAnnotations;

namespace NorthwindWebsite.Core.EmailSender;

public class AuthMessageSenderOptions
{
    [Required]
    public string SendGridApiKey { get; set; }
}
