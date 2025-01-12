using UnityEngine;
using UnityEngine.UI;

namespace ArmorVehicle.Ui
{
    public abstract class ScreenWithContinueButtonBase : UIScreen
    {
        [SerializeField] private Button _continueButton;

        private void Awake()
        {
            _continueButton.onClick.AddListener(OnStart);
        }

        private void OnDestroy()
        {
            _continueButton.onClick.RemoveListener(OnStart);
        }

        private void OnStart()
        {
            TaskCompletionSource.TrySetResult(true);
        }
    }
}