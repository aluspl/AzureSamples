using Autofac;
using LifeLike.CloudService.CosmosDB;
using LifeLike.CloudService.MongoDB;
using LifeLike.CloudService.Search;
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
            builder.RegisterType<CosmosUnitOfWork>().As<IUnitOfWork>().Keyed<IUnitOfWork>(Provider.CosmosDB);
            builder.RegisterType<SQLUnitOfWork>().As<IUnitOfWork>().Keyed<IUnitOfWork>(Provider.EF);
            builder.RegisterType<MongoUnitOfWork>().As<IUnitOfWork>().Keyed<IUnitOfWork>(Provider.MongoDB);
            builder.RegisterType<SearchService>().As<ISearchService>();
            builder.RegisterType<TableStorage.TableStorage>().As<ITableStorage>();
        }
    }
}
