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
using Microsoft.AspNetCore.Server.IISIntegration;
using FitnessTracker.Api.Configuration;

using Microsoft.AspNetCore.HttpOverrides;
using FitnessTracker.Api.Services;

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
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                 });
            });

            //services.Configure<IISOptions>(options => {});

            services.AddAuthentication(IISDefaults.AuthenticationScheme);

            services.AddDbContext<FitnessTrackerContext>(config =>
            {
                config.UseSqlServer(_config.GetConnectionString("FitnesssTrackerConn"), opt => opt.MigrationsAssembly("FitnessTracker.Api"))
                .EnableSensitiveDataLogging();
            });

            services.Configure<IISServerOptions>(options =>
            {
                options.AutomaticAuthentication = false;
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

            var jwtSetting = _config.GetSection(nameof(JwtSetting)).Get<JwtSetting>();
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
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSetting.JwtIssuer,
                        ValidAudience = jwtSetting.JwtIssuer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.JwtKey)),
                        ClockSkew = TimeSpan.Zero // remove delay of token when expire
                    };
                });

            // scans all the assemblies looking for all the classes that inherit AutoMapper.Profile class
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.Configure<JwtSetting>(_config.GetSection(nameof(JwtSetting)));
            services.Configure<EmailSetting>(_config.GetSection(nameof(EmailSetting)));

            services.AddTransient<ILogRepository, LogRepository>();
            services.AddTransient<IExerciseRepository, ExerciseRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ILogService, LogService>();
            services.AddTransient<IEmailService, EmailService>();

            services.AddControllers();

            //services.AddMvc(option => {
            //    option.EnableEndpointRouting = false;
            //    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
            //    option.Filters.Add(new AuthorizeFilter(policy));
            //}).AddNewtonsoftJson();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseCors(_allowOrigins);

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
