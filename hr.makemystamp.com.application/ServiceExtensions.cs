using FluentValidation;
using hr.makemystamp.com.application.Interfaces;
using hr.makemystamp.com.application.Service;
using hr.makemystamp.com.core.Dtos;
using hr.makemystamp.com.core.Service;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace hr.makemystamp.com.application
{
    public static class ServiceExtensions
    {
        public static void ConfigureApplication(this IServiceCollection serviceDescriptors, IConfiguration configuration)
        {
            serviceDescriptors.AddAutoMapper(Assembly.GetExecutingAssembly());
            serviceDescriptors.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            serviceDescriptors.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            serviceDescriptors.AddScoped<IPasswordService, PasswordService>();
            serviceDescriptors.AddTransient<SmtpClient>();
            serviceDescriptors.Configure<MailSettingsDto>(configuration.GetSection("MailSettings"));
            serviceDescriptors.Configure<JWTSettings>(configuration.GetSection("JWTSettings"));
            serviceDescriptors.AddScoped<ITokenBlackListService, TokenBlackListService>();
        }
    }
}
