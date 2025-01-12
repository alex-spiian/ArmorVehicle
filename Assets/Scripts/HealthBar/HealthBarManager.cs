using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ArmorVehicle
{
    public class HealthBarManager : MonoBehaviour
    {
        [SerializeField] private HealthBarData[] _healthBarData;
        [SerializeField] private RectTransform _canvasRectTransform;

        private readonly Dictionary<IHealth, HealthBar> _healthBars = new();
        private readonly Dictionary<HealthBarType, MonoBehaviourPool<HealthBar>> _healthBarPools = new();

        public void Spawn(IHealth health, HealthBarType type)
        {
            var pool = GetHealthBarPool(type);
            var healthBar = pool.Take();
            
            healthBar.Initialize(health, _canvasRectTransform);
            _healthBars.TryAdd(health, healthBar);
        }

        public void Remove(IHealth health, HealthBarType type)
        {
            if (_healthBars.TryGetValue(health, out var healthBar))
            {
                healthBar.Reset();
                var pool = GetHealthBarPool(type);
                pool.Release(healthBar);
            }
            else
            {
                Debug.LogWarning($"HealthBar of {health.Owner.name} with type {type} was not found in dictionary.");
            }
        }
        
        private MonoBehaviourPool<HealthBar> GetHealthBarPool(HealthBarType type)
        {
            if (HasCreatedPool(type))
            {
                return _healthBarPools[type];
            }

            var healthBarPrefab = GetHealthBar(type);
            var healthBarPool = new MonoBehaviourPool<HealthBar>(healthBarPrefab, transform);
        
            _healthBarPools.Add(type, healthBarPool);
            return healthBarPool;
        }

        private bool HasCreatedPool(HealthBarType type)
        {
            return _healthBarPools.ContainsKey(type);
        }

        private HealthBar GetHealthBar(HealthBarType type)
        {
            var requiredHealthBarData = _healthBarData.FirstOrDefault(data => data.Type == type);

            if (requiredHealthBarData == null)
            {
                Debug.LogError($"HealthBar with type {type} was not found. Provide HealthBarData to the HealthBarManager.");
            }
            
            return requiredHealthBarData.Prefab;
        }
    }
}