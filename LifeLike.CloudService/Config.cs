using LifeLike.CloudService.CosmosDB;
using LifeLike.CloudService.MongoDB;
using LifeLike.CloudService.ServiceBus;
using LifeLike.CloudService.SqlDB;
using LifeLike.Shared;
using LifeLike.Shared.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace LifeLike.CloudService
{
    public class Config
    {
        public static void SetupCloudService(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, CosmosUnitOfWork>();
            services.AddScoped<IUnitOfWork, SQLUnitOfWork>();
            services.AddScoped<IUnitOfWork, MongoUnitOfWork>();
            services.AddScoped<ITableStorage, TableStorage.TableStorage>();
            //services.AddScoped<IServiceBus, ServiceBusService>();
            services.AddHostedService<ServiceBusService>();

        }
    }
}
