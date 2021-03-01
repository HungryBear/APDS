using Microsoft.Extensions.Configuration;
using DistanceService.Business.Implementations;
using DistanceService.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace DistanceService.Business
{
    public class DIClient
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IDistanceEvaluationComponent, HaversineDistanceEval>();
            services.AddSingleton<IAirportDataStorage, SqlLiteStorage>(sp => new SqlLiteStorage(configuration.GetSection("ConnectionStrings:SqlLite").Value));
            services.AddScoped<DistanceMeasureService>();

        }
    }
}
