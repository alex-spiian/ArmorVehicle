using System.Linq;
using UnityEngine;

namespace ArmorVehicle
{
    [RequireComponent(typeof(IHealth))]
    public class FxHandler : MonoBehaviour
    {
        [SerializeField] private FxData[] _effectsData;
        
        private IHealthHandler _healthHandler;

        private void Start()
        {
            _healthHandler = GetComponent<IHealthHandler>();
            _healthHandler.Died += OnDied;
            _healthHandler.Damaged += OnDamaged;
        }
        
        private void OnDestroy()
        {
            _healthHandler.Died -= OnDied;
            _healthHandler.Damaged -= OnDamaged;
        }

        public void Reset()
        {
            foreach (var fxData in _effectsData)
            {
                fxData.Effect.SetActive(false);
            }
        }
        
        private void OnDied()
        {
            var deathFxPrefab = GetFx(FxType.Death);
            var fx =  Instantiate(deathFxPrefab);
            fx.transform.position = transform.position;
        }

        private void OnDamaged()
        {
           // var damagedFx = GetFx(FxType.Damage);
           // damagedFx.SetActive(true);
        }

        private GameObject GetFx(FxType type)
        {
            return _effectsData.FirstOrDefault(data => data.Type == type)?.Effect;
        }
    }
}