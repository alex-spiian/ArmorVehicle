using DG.Tweening;
using UnityEngine;

namespace ArmorVehicle
{
    public class PlayerFxHandler : FxHandlerBase
    {
        [SerializeField] private Transform _carTransform;
        [SerializeField] private float _minSarScaleFactor;
        [SerializeField] private float _maxSarScaleFactor;
        [SerializeField] private float _scaleDuration;
        
        protected override void OnDamaged()
        {
            var randomScaleFactor = Random.Range(_minSarScaleFactor, _maxSarScaleFactor);
            var sequence = DOTween.Sequence();
            sequence.Append(_carTransform.DOScale(Vector3.one * randomScaleFactor, _scaleDuration / 2));
            sequence.Append(_carTransform.DOScale(Vector3.one, _scaleDuration / 2));

            sequence.Play();
        }
        
    }
}