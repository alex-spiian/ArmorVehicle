using VContainer;
using VContainer.Unity;

namespace ArmorVehicle.Core
{
    public class GameLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            builder.RegisterEntryPoint<BootstrapEntryPoint>();
        }
    }
}