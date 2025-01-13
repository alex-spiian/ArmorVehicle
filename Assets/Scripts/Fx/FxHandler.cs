using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Pool;

namespace ArmorVehicle
{
    [RequireComponent(typeof(IHealth))]
    public class FxHandler : MonoBehaviour
    {
        [SerializeField] private GameObject _deathEffectPrefab;
        [SerializeField] private Renderer _renderer;
        [SerializeField] private float _flashDuration;

        private Material materialInstance;
        private IHealthHandler _healthHandler;
        private ObjectPool<GameObject> _effectsPool;
        private List<GameObject> _activeEffects = new();
        private readonly int _flashIntensity = Shader.PropertyToID("_FlashIntensity");

        private void Start()
        {
            _healthHandler = GetComponent<IHealthHandler>();
            _healthHandler.Died += OnDied;
            _healthHandler.Damaged += OnDamaged;
            
            materialInstance = new Material(_renderer.material);
            _renderer.material = materialInstance;

            _effectsPool = new ObjectPool<GameObject>(
                createFunc: () => Instantiate(_deathEffectPrefab),
                actionOnGet: effect => effect.gameObject.SetActive(true),
                actionOnRelease: effect => effect.gameObject.SetActive(false));
        }
        
        private void OnDestroy()
        {
            _healthHandler.Died -= OnDied;
            _healthHandler.Damaged -= OnDamaged;
        }

        public void Reset()
        {
            foreach (var activeEffect in _activeEffects)
            {
                _effectsPool.Release(activeEffect);
            }
            
            _activeEffects.Clear();
        }

        private async void Flash()
        {
            materialInstance.SetFloat(_flashIntensity, 1);
            await UniTask.WaitForSeconds(_flashDuration);
            materialInstance.SetFloat(_flashIntensity, 0);
        }
        
        private void ResetMaterial()
        {
            materialInstance.SetFloat(_flashIntensity, 0);
        }

        private void OnDied()
        {
            ResetMaterial();
            var fx = _effectsPool.Get();
            fx.transform.position = transform.position;
            _activeEffects.Add(fx);
        }

        private void OnDamaged()
        {
            Flash();
        }
    }
}