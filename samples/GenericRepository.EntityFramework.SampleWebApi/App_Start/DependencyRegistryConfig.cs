using Autofac;
using Autofac.Integration.WebApi;
using AutoMapper;
using GenericRepository.EntityFramework.SampleCore;
using GenericRepository.EntityFramework.SampleCore.Entities;
using System.Reflection;
using System.Web.Http;

namespace GenericRepository.EntityFramework.SampleWebApi.App_Start
{
    public class DependencyRegistryConfig
    {
        public static void RegisterDependencies(HttpConfiguration config, IMapper mapper)
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // Register IEntitiesContext
            builder.Register(_ => new AccommodationEntities())
                   .As<IEntitiesContext>().InstancePerApiRequest();

            // Register repositories
            builder.RegisterType<EntityRepository<Country>>()
                   .As<IEntityRepository<Country>>().InstancePerApiRequest();
            builder.RegisterType<EntityRepository<Resort>>()
                   .As<IEntityRepository<Resort>>().InstancePerApiRequest();
            builder.RegisterType<EntityRepository<Hotel>>()
                   .As<IEntityRepository<Hotel>>().InstancePerApiRequest();

            // Register IMappingEngine
            builder.Register(_ => mapper).As<IMapper>().SingleInstance();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(builder.Build());
        }
    }
}