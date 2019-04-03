using Autofac;
using LifeLike.Shared.Services;

namespace LifeLike.ElasticSearch
{
    public class ElasticSearchModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
          
            builder.RegisterType<ElasticSearchService>().As<ISearchService>();
           

        }
        
    }
}
