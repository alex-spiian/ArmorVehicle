using UnityEngine;
using UnityEngine.UI;

namespace ArmorVehicle
{
    [RequireComponent(typeof(Slider))]
    [RequireComponent(typeof(RectTransform))]
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Vector3 _offset;
        [SerializeField] private bool _hideFromStart;

        protected RectTransform RectTransform;
        private Slider _slider;
        private Camera _mainCamera;
        private Transform _target;
        private IHealth _health;
        private RectTransform _parentRectTransform;

        private void Awake()
        {
            _slider = GetComponent<Slider>();
            RectTransform = GetComponent<RectTransform>();
            _mainCamera = Camera.main;
        }
        
        private void LateUpdate()
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
            
            gameObject.SetActive(!_hideFromStart);
        }
        
        public void Reset()
        {
            _health.HealthChanged -= OnHealthChanged;
        }
        
        protected virtual void FollowTarget()
        {
            var localPoint = CalculatePosition();
            RectTransform.anchoredPosition = localPoint;
        }
        
        protected Vector2 CalculatePosition()
        {
            var pointInScreenSpace = RectTransformUtility.WorldToScreenPoint(_mainCamera, _target.position + _offset);

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _parentRectTransform, pointInScreenSpace, null, out var localPoint);
            return localPoint;
        }

        private void OnHealthChanged(float health)
        {
            HandleVisibility();

            _slider.value = health / _health.MaxHealth;
        }

        private void HandleVisibility()
        {
            if (!gameObject.activeSelf)
            {
                gameObject.SetActive(true);
            }
        }
    }
}