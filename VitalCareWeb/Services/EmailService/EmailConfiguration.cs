namespace VitalCareWeb.Services.EmailService;

public class EmailConfiguration
{
    public string From { get; set; } = String.Empty;
    public string Host { get; set; } = String.Empty;
    public int Port { get; set; }
    public string Username { get; set; } = String.Empty;
    public string Password { get; set; } = String.Empty;
}
