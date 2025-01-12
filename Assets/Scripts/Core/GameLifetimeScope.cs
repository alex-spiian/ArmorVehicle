using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ArmorVehicle.Core
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private GameControllerConfig _gameControllerConfig;
        [SerializeField] private HealthBarManager _healthBarManager;
        [SerializeField] private ScreensManager _screensManager;
        
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);
            builder.RegisterEntryPoint<GameController>();
            
            builder.Register<LevelSwitcher>(Lifetime.Scoped);
            builder.Register<InputHandler>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.RegisterInstance(_gameControllerConfig);
            builder.RegisterInstance(_healthBarManager);
            builder.RegisterInstance(_screensManager);
        }
    }
}