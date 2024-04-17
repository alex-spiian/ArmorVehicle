using UnityEngine;
using UnityEngine.UI;

namespace Health
{
    public class HealthBarView : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        
        private IHealthHandler _healthHandler;
        
        public void Initialize(IHealthHandler healthHandler)
        {
            healthHandler.HealthChanged += OnHealthChanged;
            healthHandler.Died += SetNotActive;
            
            _slider.maxValue = healthHandler.MaxHealth;
            _slider.value = healthHandler.MaxHealth;
        }

        private void OnHealthChanged(float currentHealth)
        {
            SetActive();
            _slider.value = currentHealth;
        }

        private void SetActive()
        {
            gameObject.SetActive(true);
        }
        
        private void SetNotActive()
        {
            gameObject.SetActive(false);
        }
    }
}