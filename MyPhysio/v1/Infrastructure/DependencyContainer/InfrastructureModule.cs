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
           
            #region Master Dependency Configuration
            builder.RegisterType<Repository<MasterEntityModel>>().As<IRepository<MasterEntityModel>>().InstancePerDependency();
            builder.RegisterType<ServiceClient<object, CustomerDetailServiceModelResponse>>().As<IServiceClient<object, CustomerDetailServiceModelResponse>>().InstancePerDependency();
            builder.RegisterType<ServiceClient<object, CustomerPhoneServiceModelResponse>>().As<IServiceClient<object, CustomerPhoneServiceModelResponse>>().InstancePerDependency();
            builder.RegisterType<ServiceClient<object, CustomerOrderServiceModelResponse>>().As<IServiceClient<object, CustomerOrderServiceModelResponse>>().InstancePerDependency();
            builder.RegisterType<ServiceClient<object, OrderDetailsServiceModelResponse>>().As<IServiceClient<object, OrderDetailsServiceModelResponse>>().InstancePerDependency();
            builder.RegisterType<ServiceClient<PartialUpdateRequestServiceModel, PartialUpdateResponseServiceModel>>().As<IServiceClient<PartialUpdateRequestServiceModel, PartialUpdateResponseServiceModel>>().InstancePerDependency();
            builder.RegisterType<ServiceClient<OrderUpdateRequestServiceModel, OrderUpdateResponseServiceModel>>().As<IServiceClient<OrderUpdateRequestServiceModel, OrderUpdateResponseServiceModel>>().InstancePerDependency();
            #endregion
        }
    }
}
