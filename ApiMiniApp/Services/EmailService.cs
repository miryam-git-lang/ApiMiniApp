using System.Net.Mail;

namespace ApiMiniApp.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration configuration;

    public EmailService(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public async Task SendEmailAsync(string ToEmail, string subject, string htmlMessage)
    {
        var emailSettings = configuration.GetSection("EmailSettings");
        var host = emailSettings["Host"];
        if (!int.TryParse(emailSettings["Port"], out var portNumber)) portNumber = 587;
        var senderEmail = emailSettings["SenderEmail"];
        var password = emailSettings["Password"];

        var client = new SmtpClient(host, portNumber)
        {
            Credentials = new System.Net.NetworkCredential(senderEmail, password),
            EnableSsl = true
        };
        var mailMessage = new MailMessage(senderEmail, ToEmail, subject, htmlMessage)
        {
            IsBodyHtml = true
        };

        await client.SendMailAsync(mailMessage);
    }
}