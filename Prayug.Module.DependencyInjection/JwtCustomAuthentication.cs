using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Prayug.Module.DependencyInjection
{
    
    public static class JwtCustomAuthentication
    {
        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services
        , string audience, string issuer, string apiKey)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                          .AddJwtBearer(options =>
                          {
                              options.SaveToken = true;
                              options.RequireHttpsMetadata = false;
                              options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                              {
                                  ValidateIssuer = true,
                                  ValidateAudience = true,
                                  ValidAudience = audience,
                                  ValidIssuer = issuer,
                                  IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(apiKey))
                              };
                          });
            return services;
        }
    }
}
