using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Prayug.Infrastructure;
using Prayug.Module.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prayug.Portal
{
    public class Startup
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        public Startup(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            GlobalSettings.WebRootPath = _hostingEnvironment.WebRootPath;
            GlobalSettings.ContentRootPath = _hostingEnvironment.ContentRootPath;
            GlobalSettings.LogConnection = Configuration["LogConnection"];
            GlobalSettings.ApiKey = Configuration["ApiKey"];
            GlobalSettings.Audience = Configuration["Auth:Audience"];
            GlobalSettings.Issuer = Configuration["Auth:Issuer"];
            GlobalSettings.MediaServerUrl = Configuration["MediaServerUrl"];
            #region Called for ASPNETCORE_ENVIRONMENT=Development
            var connection = Configuration.GetConnectionString("DatabaseConnection");

            services.AddDevelopmentServices(GlobalSettings.Audience, GlobalSettings.Issuer, GlobalSettings.ApiKey);
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            #endregion

            #region User
            services.AddTransient<Prayug.Module.Core.Interfaces.IUser, Prayug.Module.Core.Concrete.UserConcrete>();
            services.AddTransient<Prayug.Module.Core.Interfaces.RepositoryInterfaces.Web.IUserAuthenticationRepository, Prayug.Module.Core.Repositorys.Web.UserAuthenticationRepository>();
            services.AddTransient<Prayug.Module.Core.Interfaces.ILearning, Prayug.Module.Core.Concrete.LearningConcrete>();
            services.AddTransient<Prayug.Module.Core.Interfaces.RepositoryInterfaces.Web.ILearningRepository, Prayug.Module.Core.Repositorys.Web.LearningRepository>();
            services.AddTransient<Prayug.Module.Core.Interfaces.ITutor, Prayug.Module.Core.Concrete.TutorConcrete>();
            services.AddTransient<Prayug.Module.Core.Interfaces.RepositoryInterfaces.Web.ITutorRepository, Prayug.Module.Core.Repositorys.Web.TutorRepository>();
            services.AddTransient<Prayug.Module.Core.Interfaces.IImport, Prayug.Module.Core.Concrete.ImportConcrete>();
            services.AddTransient<Prayug.Module.Core.Interfaces.RepositoryInterfaces.Web.IImportRepository, Prayug.Module.Core.Repositorys.Web.ImportRepository>();
            services.AddTransient<Prayug.Module.Core.Interfaces.ISubject, Prayug.Module.Core.Concrete.SubjectConcrete>();
            services.AddTransient<Prayug.Module.Core.Interfaces.RepositoryInterfaces.Web.ISubjectRepository, Prayug.Module.Core.Repositorys.Web.SubjectRepository>();
            services.AddTransient<Prayug.Module.Core.Interfaces.IUnit, Prayug.Module.Core.Concrete.UnitConcrete>();
            services.AddTransient<Prayug.Module.Core.Interfaces.RepositoryInterfaces.Web.IUnitRepository, Prayug.Module.Core.Repositorys.Web.UnitRepository>();
            services.AddTransient<Prayug.Module.Core.Interfaces.ILession, Prayug.Module.Core.Concrete.LessionConcrete>();
            services.AddTransient<Prayug.Module.Core.Interfaces.RepositoryInterfaces.Web.ILessionRepository, Prayug.Module.Core.Repositorys.Web.LessionRepository>();
            services.AddTransient<Prayug.Module.Core.Interfaces.ICategory, Prayug.Module.Core.Concrete.CategoryConcrete>();
            services.AddTransient<Prayug.Module.Core.Interfaces.RepositoryInterfaces.Web.ICategoryRepository, Prayug.Module.Core.Repositorys.Web.CategoryRepository>();
            services.AddTransient<Prayug.Module.Core.Interfaces.IEnquiry, Prayug.Module.Core.Concrete.EnquiryConcrete>();
            services.AddTransient<Prayug.Module.Core.Interfaces.RepositoryInterfaces.Web.IEnquiryRepository, Prayug.Module.Core.Repositorys.Web.EnquiryRepository>();
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            var pathBase = Configuration["PATH_BASE"];
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("MyPolicy");
            //app.UseCors();
            app.UseVersionedSwagger(provider, pathBase);
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
