using FitnessTracker.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessTracker.Data.Extensions
{
    public static class IServiceCollectionExtension
    {
        public static void AddFitnessTrackerDB(this IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<ILogRepository, LogRepository>();
            services.AddTransient<IExerciseRepository, ExerciseRepository>();
            services.AddTransient<IUserRepository, UserRepository>();

            services.AddDbContextPool<FitnessTrackerContext>(options =>
                options.UseSqlServer(config.GetConnectionString(nameof(FitnessTrackerContext))
            );
        }
    }
}
