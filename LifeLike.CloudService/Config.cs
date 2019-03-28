using Autofac;
using LifeLike.CloudService.CosmosDB;
using LifeLike.CloudService.MongoDB;
using LifeLike.CloudService.ServiceBus;
using LifeLike.CloudService.SqlDB;
using LifeLike.Shared;
using LifeLike.Shared.Enums;
using LifeLike.Shared.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace LifeLike.CloudService
{
    public static class Config
    {
        public static void SetupCloudService(this ContainerBuilder services)
        {
          
        }
        public static void SetupBackground(this IServiceCollection services)
        {
            services.AddHostedService<ServiceBusService>();
        }
    }
}
