using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ArmorVehicle.Core
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private EnemySpawnerConfig _enemySpawnerConfig;
        [SerializeField] private LevelList _levelList;
        
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            builder.RegisterEntryPoint<BootstrapEntryPoint>();
            builder.Register<LevelSwitcher>(Lifetime.Scoped);

            builder.RegisterInstance(_enemySpawnerConfig);
            builder.RegisterInstance(_levelList);
        }
    }
}