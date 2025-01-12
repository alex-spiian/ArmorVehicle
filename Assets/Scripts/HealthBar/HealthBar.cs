using UnityEngine;
using UnityEngine.UI;

namespace ArmorVehicle
{
    [RequireComponent(typeof(Slider))]
    [RequireComponent(typeof(RectTransform))]
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Vector3 _offset;

        private Slider _slider;
        private RectTransform _rectTransform;
        private Camera _mainCamera;
        private Transform _target;
        private IHealth _health;
        private RectTransform _parentRectTransform;

        private void Awake()
        {
            _slider = GetComponent<Slider>();
            _rectTransform = GetComponent<RectTransform>();
            _mainCamera = Camera.main;
        }
        
        private void Update()
        {
            FollowTarget();
        }
        
        public void Initialize(IHealth health, RectTransform parentRectTransform)
        {
            _parentRectTransform = parentRectTransform;
            _health = health;
            _target = health.Owner;
            _health.HealthChanged += OnHealthChanged;
            OnHealthChanged(_health.Health);
        }
        
        public void Reset()
        {
            _health.HealthChanged -= OnHealthChanged;
        }

        private void OnHealthChanged(float health)
        {
            _slider.value = health / _health.MaxHealth;
        }
        
        private void FollowTarget()
        {
            var pointInScreenSpace = RectTransformUtility.WorldToScreenPoint(_mainCamera, _target.position + _offset);

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _parentRectTransform, pointInScreenSpace, null, out var localPoint);

            _rectTransform.anchoredPosition = localPoint;
        }
    }
}