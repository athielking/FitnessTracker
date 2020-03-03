using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitnessTracker.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using FitnessTracker.Data.Repositories;
using System.Reflection;
using AutoMapper;
using Newtonsoft.Json;
using FitnessTracker.Services;

namespace FitnessTracker
{
    public class Startup
    {
        private IConfiguration _config;

        public Startup(IConfiguration configuration)
        {
            _config = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<FitnessTrackerContext>(config =>
            {
                config.UseSqlServer(_config.GetConnectionString("FitnesssTrackerConn"));
            });

            // scans all the assemblies looking for all the classes that inherit AutoMapper.Profile class
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddTransient<ILogRepository, LogRepository>();
            services.AddTransient<IExerciseRepository, ExerciseRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ILogService, LogService>();

            services.AddControllers();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
