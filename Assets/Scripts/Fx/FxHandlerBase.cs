using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace ArmorVehicle
{
    public abstract class FxHandlerBase : MonoBehaviour
    {
        [SerializeField] private GameObject _deathEffectPrefab;

        private IHealthHandler _healthHandler;
        private ObjectPool<GameObject> _effectsPool;
        private readonly List<GameObject> _activeEffects = new();

        protected virtual void Start()
        {
            _healthHandler = GetComponent<IHealthHandler>();
            _healthHandler.Died += OnDied;
            _healthHandler.Damaged += OnDamaged;
            
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
        
        protected virtual void OnDied()
        {
            var fx = _effectsPool.Get();
            fx.transform.position = transform.position;
            _activeEffects.Add(fx);
        }

        protected abstract void OnDamaged();
    }
}