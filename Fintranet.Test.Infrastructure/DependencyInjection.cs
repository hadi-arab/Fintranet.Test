using Fintranet.Test.Application.Abstractions.Repositories;
using Fintranet.Test.Infrastructure.Data;
using Fintranet.Test.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fintranet.Test.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDBContext>(options =>
                options.UseSqlite(configuration.GetConnectionString("ApplicationConnectionString")));


            // instead of a IDistributedCache like Redis
            services.AddSingleton<IMemoryCache, MemoryCache>();
            
            services.AddScoped<ICongestionTaxCalculationRepository, CongestionTaxCalculationRepository>();
            services.AddScoped<ICongestionRuleRepository, CongestionRuleRepository>();
            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddScoped<ITollFreeVehicleRepository, TollFreeVehicleRepository>();
            services.AddScoped<ICongestionFeeRepository, CongestionFeeRepository>();
            services.AddScoped<IRemoteDataRepository, RemoteDataRepository>();

            return services;
        }
    }
}
