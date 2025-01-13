using DG.Tweening;
using UnityEngine;

namespace ArmorVehicle.Ui
{
    public class ArrowMovement : MonoBehaviour
    {
        [SerializeField] private float _moveDistance;
        [SerializeField] private float _duration;

        private void Start()
        {
            MoveArrow();
        }

        private void MoveArrow()
        {
            var startX = transform.position.x;

            var sequence = DOTween.Sequence();

            sequence.Append(transform.DOMoveX(startX - _moveDistance, _duration)
                .SetEase(Ease.InOutQuad));

            sequence.Append(transform.DOMoveX(startX, _duration)
                .SetEase(Ease.InOutQuad));

            sequence.SetLoops(-1, LoopType.Yoyo);
        }
    }
}