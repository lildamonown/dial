using Kursa4.DAL.Entities;
using Kursa4.DAL.Repositories.Implementation.EF;
using Kursa4.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;  // ДОБАВИТЬ ЭТУ СТРОКУ

namespace Kursa4.DAL.Extantions
{
    public static class ConfigureServices
    {
        public static void AddDalServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
                     .EnableRetryOnFailure(  // ДОБАВИТЬ ЭТО
                         maxRetryCount: 5,
                         maxRetryDelay: TimeSpan.FromSeconds(30),
                         errorNumbersToAdd: null)));

            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddScoped<ICarBrandRepository, CarBrandRepository>()
                .AddScoped<ICarSeriesRepository, CarSeriesRepository>()
                .AddScoped<ICarRepository, CarRepository>()
                .AddScoped<IOrderRepository, OrderRepository>()
                .AddScoped<IReportRepository, ReportRepository>()
                .AddScoped<IReviewRepository, ReviewRepository>()
                .AddScoped<IServiceRepository, ServiceRepository>()
                .AddScoped<IStatisticRepository, StatisticRepository>()
                .AddScoped<IStatusRepository, StatusRepository>()
                .AddScoped<ISubserviceRepository, SubserviceRepository>();
        }
    }
}