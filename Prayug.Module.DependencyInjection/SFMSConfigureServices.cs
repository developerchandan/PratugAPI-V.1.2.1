namespace Prayug.Module.DependencyInjection
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using System.Text.Json.Serialization;
    public static class SFMSConfigureServices
    {

        public static IServiceCollection AddDevelopmentServices(this IServiceCollection services, string audience, string issuer, string apiKey)
        {
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            services.AddControllers().AddControllersAsServices();
            //    .AddJsonOptions(opts =>
            //{
            //    opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            //});
            //services.AddCors(options =>
            //{
            //    options.AddDefaultPolicy(policy =>
            //        policy.AllowAnyOrigin()
            //            .AllowAnyHeader()
            //            .AllowAnyMethod());
            //});


            services.AddCustomAuthentication(audience, issuer, apiKey);
            services.AddVersioning();
            services.AddSwagger();


            return services;
        }
        public static IServiceCollection AddProductionServices(this IServiceCollection services, string audience, string issuer, string apiKey)
        {

            services.AddControllers().AddControllersAsServices();
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
            services.AddCustomAuthentication(audience, issuer, apiKey);
            services.AddVersioning();
            services.AddSwagger();

            return services;
        }
    }
}
