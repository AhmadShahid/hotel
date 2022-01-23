using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFynd.Application.Contracts;
using UFynd.Infrastructure.Repositories;

namespace UFynd.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IHotelRateRepository, HotelRepository>();
            services.AddScoped(typeof(IDataReader<>), typeof(JsonDataReader<>));
            return services;
        }
    }
}
