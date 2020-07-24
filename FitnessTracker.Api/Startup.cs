using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using System.Reflection;
using AutoMapper;

using FitnessTracker.Services;
using FitnessTracker.Data;
using FitnessTracker.Data.Repositories;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication.JwtBearer;

using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;
using Microsoft.AspNetCore.Identity;
using FitnessTracker.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using FitnessTracker.Api.Extenisons;
using FitnessTracker.Api.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
                            options.AddPolicy(_allowOrigins,
                             builder =>
                             {
                                 builder.WithOrigins("http://localhost:4200")

                                 //.WithHeaders(HeaderNames.ContentType, "application/json")
                                 .AllowAnyHeader()
                                 //.SetIsOriginAllowed((host) => true)

                                 .AllowCredentials()
                                 .AllowAnyMethod();
                             });
                        });

            services.AddDbContext<FitnessTrackerContext>(config =>
            {
                config.UseSqlServer(_config.GetConnectionString("FitnesssTrackerConn"), opt => opt.MigrationsAssembly("FitnessTracker.Api"))
                .EnableSensitiveDataLogging();
            });

            services.AddIdentity<User, IdentityRole>(options => {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 1;
            }).AddEntityFrameworkStores<FitnessTrackerContext>()
              .AddDefaultTokenProviders();

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                })
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = _config["JwtIssuer"],
                        ValidAudience = _config["JwtIssuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtKey"])),
                        ClockSkew = TimeSpan.Zero // remove delay of token when expire
                    };
                });

            // scans all the assemblies looking for all the classes that inherit AutoMapper.Profile class
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddTransient<ILogRepository, LogRepository>();
            services.AddTransient<IExerciseRepository, ExerciseRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ILogService, LogService>();

            services.AddControllers();

            services.AddMvc(option => {
                option.EnableEndpointRouting = false;
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                option.Filters.Add(new AuthorizeFilter(policy));
            }).AddNewtonsoftJson();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(_allowOrigins);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseHttpsRedirection();

            app.UseCors();

            app.UseAuthorization();



            app.UseMvc();
        }
    }
}
