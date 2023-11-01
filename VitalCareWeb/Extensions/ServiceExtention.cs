using VitalCareWeb.Services.EmailService;

namespace VitalCareWeb.Extensions
{
    public static class ServiceExtention
    {
        public static void ConfigureEmailService(this IServiceCollection services, IConfiguration configuration)
        {
            var emailConfig = configuration
                 .GetSection("EmailConfiguration")
                 .Get<EmailConfiguration>();
            services.AddSingleton(emailConfig);
        }
    }
}
