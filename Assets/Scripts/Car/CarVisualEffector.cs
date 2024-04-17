using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using Health;
using Scripts;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Car
{
    public class CarVisualEffector : VisualEffector
    {
        [SerializeField] protected float _speedForce;
        [SerializeField] private Collider _collider;
        private Rigidbody _rigidbody;
        
        [SerializeField]
        private Image _image;
        [SerializeField]
        private float _fadeDuration = 1f;
        [SerializeField]
        private float _targetAlpha = 0.5f;

        private Color _initialColor;
        private Color _targetColor;
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _healthHandler = GetComponent<IHealthHandler>();
            _healthHandler.Died += OnDied;
            _healthHandler.Damaged += OnDamaged;
            
            _initialColor = _image.color;
            _targetColor = new Color(_initialColor.r, _initialColor.g, _initialColor.b, _targetAlpha);
        }

        public void OnRestarted()
        {
           // _rigidbody.isKinematic = true;
           // _collider.isTrigger = true;
           // _rigidbody.velocity = Vector3.zero;
           _effect.SetActive(false);

        }
        protected override async UniTask ShowDeath()
        {
            // when the car goes up and then falls down it  touch a road collider again and it goes forward
            // because of real time map generating
            // gotta think how to solve that
            
           //_rigidbody.isKinematic = false;
           //_collider.isTrigger = false;
            //_rigidbody.AddForce(Vector3.up * _speedForce, ForceMode.VelocityChange);
           _effect.SetActive(true);
           await UniTask.DelayFrame(1);
        }

        protected override async UniTask ShowDamage()
        {
            await ChangeTransparency(_initialColor, _targetColor);
            await ChangeTransparency(_targetColor, _initialColor);
        }
        
        private async UniTask ChangeTransparency(Color initialColor, Color targetColor)
        {
            float elapsedTime = 0f;
            while (elapsedTime < _fadeDuration)
            {
                float t = elapsedTime / _fadeDuration;
                _image.color = Color.Lerp(initialColor, targetColor, t);
                await UniTask.Yield();
                elapsedTime += Time.deltaTime;
            }
            _image.color = targetColor;
        }
    }
}