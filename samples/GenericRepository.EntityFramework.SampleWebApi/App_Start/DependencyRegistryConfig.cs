using Autofac;
using Autofac.Integration.WebApi;
using AutoMapper;
using GenericRepository.EntityFramework.SampleCore;
using GenericRepository.EntityFramework.SampleCore.Entities;
using GenericRepository.EntityFramework.SampleCore.Services;
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
                   .As<IEntitiesContext>().InstancePerRequest();

            // Register repositories
            builder.RegisterType<EntityRepository<Country>>()
                   .As<IEntityRepository<Country, int>>().InstancePerRequest();
            builder.RegisterType<EntityRepository<Resort>>()
                   .As<IEntityRepository<Resort>>().InstancePerRequest();
            builder.RegisterType<EntityRepository<Hotel>>()
                   .As<IEntityRepository<Hotel>>().InstancePerRequest();

            builder.RegisterType<CountryService>()
                .As<GenericService.Services.GenericService<Country, int>>().InstancePerRequest();

            // Register IMappingEngine
            builder.Register(_ => mapper).As<IMapper>().SingleInstance();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(builder.Build());
        }
    }
}