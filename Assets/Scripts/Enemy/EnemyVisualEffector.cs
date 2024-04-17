using Cysharp.Threading.Tasks;
using Health;
using Scripts;
using UnityEngine;
using UnityEngine.Pool;

namespace Enemy
{
    public class EnemyVisualEffector : VisualEffector
    {
        private Material _material;
        private Color _initialColor;
        private ObjectPool<GameObject> _effectPool;

        private void Start()
        {
            _healthHandler = GetComponent<IHealthHandler>();
            _healthHandler.Died += OnDied;
            _healthHandler.Damaged += OnDamaged;

            _effectPool = new ObjectPool<GameObject>(
                createFunc: () => Instantiate(_effect),
                actionOnGet: effect => effect.gameObject.SetActive(true),
                actionOnRelease: effect => effect.gameObject.SetActive(false));
        }
        protected override async UniTask ShowDeath()
        {
            var effect = _effectPool.Get();
            effect.transform.position = transform.position;
            await UniTask.Delay(500);
            _effectPool.Release(effect);
        }

        protected override async UniTask ShowDamage()
        {
            // gotta figure out how to change enemy's color but only of one enemy (not all using the material)
            
           // _material.color = Color.white;
           // await UniTask.Delay(100);
           // _material.color = _initialColor;
           await UniTask.DelayFrame(1);
        }
    }
}