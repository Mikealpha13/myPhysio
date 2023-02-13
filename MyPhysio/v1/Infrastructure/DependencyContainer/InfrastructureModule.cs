using MyPhysio.Domain.EntityModels;
using MyPhysio.Domain.ServiceModels;
using MyPhysio.Infrastructure.Contracts;
using MyPhysio.Infrastructure.Repositories;
using MyPhysio.Infrastructure.Repositories.Connection;
using MyPhysio.Infrastructure.Services;
using Autofac;
using MyPhysio.Domain.ServiceModels.Request;

namespace MyPhysio.v1.Infrastructure.DependencyContainer
{

    /// <summary>
    /// This class is used to register trhe dependencies of infrastruture 
    /// </summary>
    public class InfrastructureModule:Module
    {

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<DatabaseConnections>().As<IDBConnectionFactory>().InstancePerDependency();
           
           
        }
    }
}
