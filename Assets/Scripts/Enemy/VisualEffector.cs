using Cysharp.Threading.Tasks;
using Health;
using UnityEngine;
using UnityEngine.Pool;

namespace Enemy
{
    public class VisualEffector : MonoBehaviour
    {
        [SerializeField] private HealthController _healthController;
        [SerializeField] private GameObject _effectPrefab;

        private ObjectPool<GameObject> _effectPool;
        private void Start()
        {
            _healthController.Died += OnDied;
            _healthController.Damaged += ShowDamage;
            
            _effectPool = new ObjectPool<GameObject>(
                createFunc: () => Instantiate(_effectPrefab),
                actionOnGet: t => t.gameObject.SetActive(true),
                actionOnRelease: t => t.gameObject.SetActive(false));
        }

        private void OnDied()
        {
            ShowDeath().Forget();
        }
        private async UniTask ShowDeath()
        {
            var effect = _effectPool.Get();
            effect.transform.position = transform.position;
            await UniTask.Delay(500);
            _effectPool.Release(effect);
        }

        private void ShowDamage()
        {
            // change character color to white and than back
        }

        private void OnDestroy()
        {
            _healthController.Died -= OnDied;
            _healthController.Damaged -= ShowDamage;
        }
    }
}