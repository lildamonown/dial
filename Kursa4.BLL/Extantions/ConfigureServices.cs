using Kursa4.BLL.Services.Implimentations;
using Kursa4.BLL.Services.Interfaces;
using Kursa4.DAL.Extantions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Kursa4.BLL.Extantions
{
    public static class ConfigureServices
    {
        public static void AddBllServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDalServices(configuration);  // ЭТА СТРОКА ДОЛЖНА БЫТЬ

            services.AddScoped<ICarBrandService, CarBrandService>()
                .AddScoped<ICarSeriesService, CarSeriesService>()
                .AddScoped<ICarService, CarService>()
                .AddScoped<IOrderService, OrderService>()
                .AddScoped<IPriceHistoryService, PriceHistoryService>()
                .AddScoped<IReportService, ReportService>()
                .AddScoped<IReviewService, ReviewService>()
                .AddScoped<IServiceService, ServiceService>()
                .AddScoped<IStatisticService, StatisticService>()
                .AddScoped<IStatusService, StatusService>()
                .AddScoped<ISubserviceService, SubserviceService>()
                .AddScoped<IAccountService, AccountService>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}