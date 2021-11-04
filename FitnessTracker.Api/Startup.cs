using System.Reflection;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Logging;

using AutoMapper;

using FitnessTracker.Data.Repositories;
using FitnessTracker.Api.Configuration;
using FitnessTracker.Api.Services;
using FitnessTracker.Data.Extensions;
using FitnessTracker.Api.Models.Settings;

namespace FitnessTracker
{
    public class Startup
    {
        private readonly string _corsPolicy = "defaultPolicy";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(_corsPolicy,
                 builder =>
                 {
                     builder.WithOrigins(Configuration["clienturl"])
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                 });
            });

            IdentityModelEventSource.ShowPII = true;

            services.AddMicrosoftIdentityWebApiAuthentication(Configuration, "AzureAd");

            services.AddFitnessTrackerDB(Configuration);

            //services.Configure<MicrosoftGraphApi>(
            //    options => Configuration.GetSection(nameof(MicrosoftGraphApi)).Bind(options));

            // scans all the assemblies looking for all the classes that inherit AutoMapper.Profile class
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.Configure<EmailSetting>(Configuration.GetSection(nameof(EmailSetting)));

            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IWorkoutRepository, WorkoutRepository>();

            services.AddTransient<IMicrosoftGraphService, MicrosoftGraphService>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(_corsPolicy);

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
