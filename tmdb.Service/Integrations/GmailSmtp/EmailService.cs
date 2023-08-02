using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using tmdb.Core.Utilities.Security.Encryption;
using tmdb.Service.ExternalServices;
using tmdb.Service.Integrations.GmailSmtp.Model;

namespace tmdb.Service.Integrations.GmailSmtp
{
    public class EmailService : IEmailService
    {
        private readonly string _emailGmail;
        private readonly string _passwordGmail;
        public EmailService(IConfiguration configuration)
        {
            _emailGmail = CustomCryptor.GetDecryptedContent(configuration["GmailSmtp:Email"]);
            _passwordGmail = CustomCryptor.GetDecryptedContent(configuration["GmailSmtp:Password"]);
        }

        public async Task<bool> SendEmailAsync(string to, IEmailContentModel templateModel)
        {
            try
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(_emailGmail);
                mailMessage.Subject = "Film Reminder";
                mailMessage.To.Add(new MailAddress(to));
                mailMessage.Body = $"<!DOCTYPE html><html><head></head><body><table style=\"font-family: Arial, sans-serif; border: 1px solid #ddd; margin: 20px 0; padding: 20px; border-radius: 10px; width: 100%;\"><tr><td style=\"width: 20%;\"><img src=\"{templateModel.poster_uri}\" alt=\"Movie Poster\" style=\"width:100%;\"/></td><td style=\"vertical-align: top; padding-left: 20px;\"><h1 style=\"font-size: 1.5em; color: #333;\">{templateModel.film_name}</h1><p style=\"font-size: 1.2em; color: #007BFF;\">Rating: {templateModel.rating}</p><p style=\"color: #666;\">{templateModel.wiki_description}</p></td></tr></table></body></html>\r\n";
                mailMessage.IsBodyHtml = true;

                using (var smtpClient = new SmtpClient("smtp.gmail.com") { Port = 587, Credentials = new NetworkCredential(_emailGmail, _passwordGmail), EnableSsl = true })
                {
                    await smtpClient.SendMailAsync(mailMessage);
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
