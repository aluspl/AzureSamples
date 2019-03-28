using Autofac;
using LifeLike.CloudService.CosmosDB;
using LifeLike.CloudService.MongoDB;
using LifeLike.CloudService.SqlDB;
using LifeLike.Shared;
using LifeLike.Shared.Enums;
using LifeLike.Shared.Services;

namespace LifeLike.CloudService
{
    public class CloudModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CosmosUnitOfWork>().As<IUnitOfWork>().Named<IUnitOfWork>(Provider.CosmosDB.ToString());
            builder.RegisterType<SQLUnitOfWork>().As<IUnitOfWork>().Named<IUnitOfWork>(Provider.EF.ToString());
            builder.RegisterType<MongoUnitOfWork>().As<IUnitOfWork>().Named<IUnitOfWork>(Provider.MongoDB.ToString());
            builder.RegisterType<TableStorage.TableStorage>().As<ITableStorage>();
        }
    }
}
