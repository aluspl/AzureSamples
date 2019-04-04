using Autofac;
using Autofac.Features.AttributeFilters;
using LifeLike.CloudService;
using PGSUpskill.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PGSUpskill
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.SetupCloudService();
            builder.RegisterType<MongoController>().WithAttributeFiltering();

            builder.RegisterType<CosmosController>().WithAttributeFiltering();
            builder.RegisterType<SQLController>().WithAttributeFiltering();

        }
    }
}
