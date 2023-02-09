using MyPhysio.Domain.EntityModels;
using MyPhysio.Domain.ServiceModels;
using MyPhysio.v1.Contracts;

using MyPhysio.v1.Processor.Response;
using MyPhysio.v1.ViewModels.Request;
using MyPhysio.v1.ViewModels.Response;
using Autofac;
using MyPhysioAPI.v1.ViewModels.Response;
using MyPhysioAPI.v1.Processor.Response.Master;
using MyPhysioAPI.v1.Processor.Request;
using MyPhysioAPI.v1.ViewModels.Request;
using MyPhysio.Domain.ServiceModels.Request;

namespace MyPhysio.v1.Infrastructure.DependencyContainer
{

    /// <summary>
    /// This class is used to register the dependencies of API 
    /// </summary>
    public class APIModule: Module
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            #region  Dependency Configuration
           
            builder.RegisterType<CustomerPhoneResponseProcessor>().As<IResponseProcessor<CustomerPhoneResponseViewModel, CustomerPhoneServiceModelResponse>>().InstancePerDependency();
            builder.RegisterType<CustomerOrderResponseProcessor>().As<IResponseProcessor<CustomerOrderResponseViewModel, CustomerOrderServiceModelResponse>>().InstancePerDependency();
            builder.RegisterType<PartialUpdateResponseProcessor>().As<IResponseProcessor<PartialUpdateResponseViewModel, PartialUpdateResponseServiceModel>>().InstancePerDependency();
            builder.RegisterType<PartialUpdateRequestProcessor>().As<IRequestProcessor<PartialUpdateRequestServiceModel, PartialUpdateRequestViewModel>>().InstancePerDependency();
            builder.RegisterType<OrderUpdateRequestProcessor>().As<IRequestProcessor<OrderUpdateRequestServiceModel, OrderUpdateRequestViewModel>>().InstancePerDependency();
            builder.RegisterType<OrderUpdateResponseProcessor>().As<IResponseProcessor<OrderUpdateResponseViewModel, OrderUpdateResponseServiceModel>>().InstancePerDependency();
            #endregion
        }
    }
}
