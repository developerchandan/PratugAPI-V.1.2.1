namespace Prayug.Module.DependencyInjection
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Mvc.ApiExplorer;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Prayug.Module.DependencyInjection.DependencyFilters;
    using Swashbuckle.AspNetCore.SwaggerGen;
    public static class SwaggerExtensions
    {
        //private static string XmlCommentsFilePath
        //{
        //    get
        //    {
        //        string basePath = PlatformServices.Default.Application.ApplicationBasePath;
        //        string fileName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name + ".xml";
        //        return Path.Combine(basePath, fileName);
        //    }
        //}

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen(
                options =>
                {
                    // add a custom operation filter which sets default values
                    options.OperationFilter<SwaggerDefaultValues>();

                    // integrate xml comments
                    //options.IncludeXmlComments(XmlCommentsFilePath);
                });

            return services;
        }

        public static IApplicationBuilder UseVersionedSwagger(this IApplicationBuilder app, IApiVersionDescriptionProvider provider, string pathBase)
        {

            app.UseSwagger();
            app.UseSwaggerUI(
                options =>
                {
                    // build a swagger endpoint for each discovered API version
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint($"{ (!string.IsNullOrEmpty(pathBase) ? pathBase : string.Empty) }/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                    }
                });

            return app;
        }
    }
}
