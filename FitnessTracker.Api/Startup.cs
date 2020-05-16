using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Microsoft.EntityFrameworkCore;
using System.Reflection;
using AutoMapper;

using FitnessTracker.Services;
using FitnessTracker.Data;
using FitnessTracker.Data.Repositories;
using Microsoft.Net.Http.Headers;

namespace FitnessTracker
{
    public class Startup
    {
        private readonly string _allowOrigins = "_myAllowSpecificOrigins"; 

        private IConfiguration _config;

        public Startup(IConfiguration configuration)
        {
            _config = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: _allowOrigins,
                 builder =>
                 {
                     builder.WithOrigins("http://localhost:4200")
                     .WithHeaders(HeaderNames.ContentType, "application/json")
                     .WithMethods("PUT", "DELETE");
                 });
            });

            services.AddDbContext<FitnessTrackerContext>(config =>
            {
                config.UseSqlServer(_config.GetConnectionString("FitnesssTrackerConn"), opt => opt.MigrationsAssembly("FitnessTracker.Api"))
                .EnableSensitiveDataLogging();
            });

            // scans all the assemblies looking for all the classes that inherit AutoMapper.Profile class
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddTransient<ILogRepository, LogRepository>();
            services.AddTransient<IExerciseRepository, ExerciseRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ILogService, LogService>();

            services.AddControllers();
            services.AddMvc(option => option.EnableEndpointRouting = false)
                .AddNewtonsoftJson();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthorization();
            app.UseHttpsRedirection();
            app.UseCors(_allowOrigins);
            app.UseMvc();
        }
    }
}
