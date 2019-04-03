using Autofac;
using LifeLike.CloudService;
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
        }
    }
}
