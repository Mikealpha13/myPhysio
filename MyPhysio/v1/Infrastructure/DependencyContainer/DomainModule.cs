
using Autofac;

namespace MyPhysio.v1.Infrastructure.DependencyContainer
{

    /// <summary>
    /// This class is used to register the dependecies of Domain Library
    /// </summary>
    public class DomainModule : Module
    {

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
        }
    }
}
