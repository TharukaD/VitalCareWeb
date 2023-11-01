namespace VitalCareWeb.Services.EmailService;

public interface IEmailService
{
    bool SendEmail(EmailDto request);
    string GetHTMLEmailContent(string header, string content);
}
