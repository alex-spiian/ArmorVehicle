using Health;
using UnityEngine;

namespace Order
{
    public class HealthBarPositionController : MonoBehaviour
    {
        [SerializeField] private RectTransform _uiElement;
        [SerializeField] private Vector3 _ofset;
        [SerializeField] private HealthBarView _healthBarView;

        private Transform _uiElementRoot;
        private RectTransform _canvas;


        public void Initialize(RectTransform uiElement, Transform uiElementRoot)
        {
            _healthBarView.Initialize(uiElementRoot.GetComponent<IHealthHandler>());
            _canvas = uiElement;
            _uiElementRoot = uiElementRoot;
        }
        private void Update()
        {
            if (_uiElementRoot == null)
            {
                return;
            }
            
            var pointInScreenSpace = RectTransformUtility.WorldToScreenPoint(UnityEngine.Camera.main, _uiElementRoot.position + _ofset);

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _canvas, pointInScreenSpace, null, out var localPoint);

            _uiElement.anchoredPosition = localPoint;
        }
    }
}