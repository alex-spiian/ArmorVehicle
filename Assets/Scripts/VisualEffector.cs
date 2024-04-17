using Cysharp.Threading.Tasks;
using Health;
using UnityEngine;

namespace Scripts
{
    public abstract class VisualEffector : MonoBehaviour
    {
        protected IHealthHandler _healthHandler;
        
        [SerializeField]
        protected GameObject _effect;
        
        protected abstract UniTask ShowDeath();

        protected abstract UniTask ShowDamage();

        protected virtual void OnDied()
        {
            ShowDeath().Forget();
        }

        protected virtual void OnDamaged()
        {
            ShowDamage().Forget();
        }
        
        protected virtual void OnDestroy()
        {
            _healthHandler.Died -= OnDied;
            _healthHandler.Damaged -= OnDamaged;
        }
    }
}