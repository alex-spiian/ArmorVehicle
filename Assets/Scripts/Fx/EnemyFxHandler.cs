using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ArmorVehicle
{
    [RequireComponent(typeof(IHealth))]
    public class EnemyFxHandler : FxHandlerBase
    {
        [SerializeField] private Renderer _renderer;
        [SerializeField] private float _flashDuration;

        private Material materialInstance;
        private readonly int _flashIntensity = Shader.PropertyToID("_FlashIntensity");

        protected override void Start()
        {
            base.Start();
            
            materialInstance = new Material(_renderer.material);
            _renderer.material = materialInstance;
        }
        
        private void ResetMaterial()
        {
            materialInstance.SetFloat(_flashIntensity, 0);
        }

        protected override async void OnDamaged()
        {
            materialInstance.SetFloat(_flashIntensity, 1);
            await UniTask.WaitForSeconds(_flashDuration);
            materialInstance.SetFloat(_flashIntensity, 0);
        }

        protected override void OnDied()
        {
            ResetMaterial();
            base.OnDied();
        }
    }
}