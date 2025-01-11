using UnityEngine;

namespace ArmorVehicle
{
    [RequireComponent(typeof(HealthController))]
    public class Enemy : MonoBehaviour
    {
        private HealthController _healthController;

        private void Awake()
        {
            _healthController = GetComponent<HealthController>();
        }

        public void Initialize(float maxHealth)
        {
            _healthController.Initialize(maxHealth);
        }
    }
}