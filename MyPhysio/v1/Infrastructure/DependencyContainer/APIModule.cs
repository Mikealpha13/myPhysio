using Autofac;

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
           
        }
    }
}
