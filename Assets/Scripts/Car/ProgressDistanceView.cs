using UnityEngine;
using UnityEngine.UI;

namespace Car
{
    public class ProgressDistanceView : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private MovementController _movementController;
        private bool _isActive;

        private void Awake()
        {
            _movementController.StartedMoving += OnStartedMoving;
        }

        private void Update()
        {
            if (!_isActive) return;
            _slider.value = _movementController.CalculateMovementProgress();
        }

        private void OnStartedMoving()
        {
            _isActive = true;
        }

        private void OnDestroy()
        {
            _movementController.StartedMoving -= OnStartedMoving;

        }
    }
}