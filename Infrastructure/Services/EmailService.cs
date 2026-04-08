using Application.Interfaces;
using Application.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _config;

    public EmailService(IConfiguration config)
    {
        _config = config;
    }
    public async Task SendEmail(string to, string subject, string body)
    {
        var host = _config["EmailSettings:Host"]
            ?? throw new InvalidOperationException("EmailSettings:Host is not configured.");
        var email = _config["EmailSettings:Email"]
            ?? throw new InvalidOperationException("EmailSettings:Email is not configured.");
        var password = _config["EmailSettings:Password"]
            ?? throw new InvalidOperationException("EmailSettings:Password is not configured.");
        var port = int.Parse(_config["EmailSettings:Port"] ?? "587");
        var displayName = _config["EmailSettings:DisplayName"] ?? email;

        var smtpClient = new SmtpClient(host)
        {
            Port = port,
            Credentials = new NetworkCredential(email, password),
            EnableSsl = true
        };

        var message = new MailMessage
        {
            From = new MailAddress(email, displayName),
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };

        message.To.Add(to);
        await smtpClient.SendMailAsync(message);
    }
}