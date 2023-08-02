using tmdb.Service.Integrations.GmailSmtp.Model;

namespace tmdb.Service.ExternalServices
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(string to, IEmailContentModel templateModel);
    }
}
