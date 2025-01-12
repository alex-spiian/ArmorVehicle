using UnityEngine;
using UnityEngine.UI;

namespace ArmorVehicle
{
    [RequireComponent(typeof(Slider))]
    [RequireComponent(typeof(RectTransform))]
    public class HealthBarWithSmoothness : HealthBar
    {
        [SerializeField] private float _smoothedSpeed;

        private Vector2 _smoothedPosition;

        protected override void FollowTarget()
        {
            var localPoint = CalculatePosition();

            _smoothedPosition = Vector2.Lerp(_smoothedPosition, localPoint, Time.deltaTime * _smoothedSpeed);
            RectTransform.anchoredPosition = _smoothedPosition;
        }
    }
}