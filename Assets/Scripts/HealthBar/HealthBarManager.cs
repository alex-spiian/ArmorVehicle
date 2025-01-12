using System.Collections.Generic;
using System.Linq;
using ArmorVehicle;
using UnityEngine;

namespace HealthBar
{
    public class HealthBarManager : MonoBehaviour
    {
        [SerializeField] private HealthBarData[] _healthBarData;
        [SerializeField] private RectTransform _canvasRectTransform;

        private Dictionary<IHealth, HealthBar> _healthBars = new();

        public void Spawn(IHealth health, HealthBarType type)
        {
            var prefab = GetHealthBar(type);
            var healthBar = Instantiate(prefab, transform);
            healthBar.Initialize(health, _canvasRectTransform);

            _healthBars.TryAdd(health, healthBar);
        }

        public void Remove(IHealth health)
        {
            var healthBar = _healthBars[health];
            Destroy(healthBar.gameObject);
            _healthBars.Remove(health);
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