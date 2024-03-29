﻿using FitnessTracker.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Azure;
using System;
using Azure.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.Azure.Services.AppAuthentication;

namespace FitnessTracker.Data.Extensions
{
    public static class IServiceCollectionExtension
    {
        public static void AddFitnessTrackerDB(this IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<ILogRepository, LogRepository>();
            services.AddTransient<IExerciseRepository, ExerciseRepository>();
            services.AddTransient<IUserRepository, UserRepository>();

            services.AddDbContext<FitnessTrackerContext>(options =>
            {
               options.UseSqlServer(config.GetConnectionString(nameof(FitnessTrackerContext)))
                      .AddInterceptors(new ConnectionInterceptor(config.GetSection("PartialDns").Value));
            });
        }
    }
}
