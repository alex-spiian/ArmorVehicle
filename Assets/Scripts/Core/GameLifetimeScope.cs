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
        [SerializeField] private CameraController _cameraController;

        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);
            RegisterInputHandler(builder);
            builder.RegisterEntryPoint<GameController>();
            builder.Register<LevelSwitcher>(Lifetime.Scoped);
            
            builder.RegisterInstance(_gameControllerConfig);
            builder.RegisterInstance(_healthBarManager);
            builder.RegisterInstance(_screensManager);
            builder.RegisterInstance(_cameraController);
        }

        private void RegisterInputHandler(IContainerBuilder builder)
        {
#if UNITY_STANDALONE || UNITY_EDITOR
            builder.Register<StandaloneInputHandler>(Lifetime.Singleton)
                .As<IInputHandler>()
                .As<ITickable>();
#elif UNITY_IOS || UNITY_ANDROID
            builder.Register<MobileInputHandler>(Lifetime.Singleton)
                   .As<IInputHandler>()
                   .As<ITickable>();
#endif
        }
    }
}