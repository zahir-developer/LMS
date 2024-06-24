using System.ComponentModel;
using static LMS.Application.Constants.ConstEnum;
using Microsoft.Extensions.Logging;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using LMS.Application.Config;

namespace LMS.Application.Services;

public class EmailService : IEmailService
{
    private readonly EmailConfig _emailConfig;
    private readonly ILogger<EmailService> _logger;
    static bool mailSent = false;

    public EmailService(EmailConfig emailConfig)
    {
        _emailConfig = emailConfig;
    }

    public void SendEmail(EmailDto email)
    {
        string emailBody = GetMailContent(email.EmailType, email.EmailKeyValues);
        SendSMTPMail(email.Email, emailBody, email.Subject);
    }

    public void SendSMTPMail(string toEmail, string emailBody, string subject)
    {
        var message = new MimeMessage
        {
            From = { MailboxAddress.Parse(_emailConfig.EmailId) },
            To = { MailboxAddress.Parse(toEmail) },
            Subject = subject,
            Body = new TextPart(TextFormat.Html)
            {
                Text = emailBody
            }
        };

        using (var client = new SmtpClient())
        {
            client.Connect(_emailConfig.Host, _emailConfig.Port, SecureSocketOptions.StartTls);

            // Note: only needed if the SMTP server requires authentication
            try
            {
                client.Authenticate(_emailConfig.EmailId, _emailConfig.Password);
            }
            catch (Exception ex)
            {

            }

            client.Send(message);
            client.Disconnect(true);
        }
    }

    public string GetMailContent(EmailHtmlTemplate emailType, Dictionary<string, string> keyValues)
    {
        string filePath = _emailConfig.HtmlTemplatePath;
        string subPath = filePath + emailType.ToString() + ".html";

        string mailText = string.Empty;
        if (File.Exists(subPath))
        {
            StreamReader str = new StreamReader(subPath);
            mailText = str.ReadToEnd();

            foreach (var item in keyValues)
            {
                mailText = mailText.Replace(item.Key, item.Value);
            }
            str.Close();
        }

        return mailText == string.Empty ? "No match found !" : mailText;
    }

    private void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
    {
        // Get the unique identifier for this asynchronous operation.
        String token = (string)e.UserState;

        if (e.Cancelled)
        {
            //_logger.LogWarning("[{0}] Send canceled.", token);
        }
        if (e.Error != null)
        {
            //_logger.LogError("[{0}] {1}", token, e.Error.ToString());
        }
        else
        {
            //_logger.LogInformation("Message sent.");
        }
        mailSent = true;
    }

}
