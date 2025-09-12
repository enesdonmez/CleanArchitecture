using CleanArchitecture.Application.Services;
using System.Net;
using System.Net.Mail;

namespace CleanArchitecture.Infrastructure.Services;

public sealed class MailService : IMailService
{
    public async Task SendMailAsync(List<string> emails, string subject, string body, List<Attachment> attachments)
    {
        var mailMessage = new MailMessage
        {
            Subject = subject,
            Body = body,
            IsBodyHtml = true,
            From = new MailAddress("your email", "Your Name"),
        };

        foreach (var email in emails)
        {
            mailMessage.To.Add(email);
        }

        if (attachments != null)
        {
            foreach (var attachment in attachments)
            {
                mailMessage.Attachments.Add(attachment);
            }
        }
        using (var smtpClient = new SmtpClient("smtp.yourserver.com", 587)) 
        {
            smtpClient.Credentials = new NetworkCredential("your email", "your password");
            smtpClient.EnableSsl = true;

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
