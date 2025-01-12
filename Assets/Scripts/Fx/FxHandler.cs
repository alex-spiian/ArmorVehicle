using System.Collections;
using System.Linq;
using UnityEngine;

namespace ArmorVehicle
{
    [RequireComponent(typeof(IHealth))]
    public class FxHandler : MonoBehaviour
    {
        [SerializeField] private FxData[] _effectsData;
        [SerializeField] private Renderer _renderer;
        [SerializeField] private float _flashDuration;

        private Material materialInstance;
        private IHealthHandler _healthHandler;
        private readonly int _flashIntensity = Shader.PropertyToID("_FlashIntensity");

        private void Start()
        {
            _healthHandler = GetComponent<IHealthHandler>();
            _healthHandler.Died += OnDied;
            _healthHandler.Damaged += OnDamaged;
            
            materialInstance = new Material(_renderer.material);
            _renderer.material = materialInstance;
        }
        
        private void OnDestroy()
        {
            _healthHandler.Died -= OnDied;
            _healthHandler.Damaged -= OnDamaged;
        }
        
        private void Flash()
        {
            StopAllCoroutines();
            StartCoroutine(FlashCoroutine());
        }
        
        public void Reset()
        {
            materialInstance.SetFloat(_flashIntensity, 0);
        }

        private IEnumerator FlashCoroutine()
        {
            materialInstance.SetFloat(_flashIntensity, 1);
            yield return new WaitForSeconds(_flashDuration);
            materialInstance.SetFloat(_flashIntensity, 0);
        }
        
        
        private void OnDied()
        {
            Reset();
            var deathFxPrefab = GetFx(FxType.Death);
            var fx =  Instantiate(deathFxPrefab);
            fx.transform.position = transform.position;
        }

        private void OnDamaged()
        {
            Flash();
        }

        private GameObject GetFx(FxType type)
        {
            return _effectsData.FirstOrDefault(data => data.Type == type)?.Effect;
        }
    }
}