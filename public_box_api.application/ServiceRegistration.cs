using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace public_box_api.application
{
    public static class ServiceRegistration
    {
        public static void AddSharedApplication(this IServiceCollection services, IConfiguration _config)
        {
            //services.Configure<MailSettings>(_config.GetSection("MailSettings"));
            //services.AddTransient<IAuthenticationService, AuthenticationService>();
            //services.AddTransient<IPasswordService, PasswordService>();
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        }
    }
}
