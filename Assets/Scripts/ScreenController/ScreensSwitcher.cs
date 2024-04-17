using UnityEngine;

namespace ScreenController
{
    public class ScreensSwitcher : MonoBehaviour
    {
        [SerializeField] private GameObject _darkEffect;
        [SerializeField] private GameObject _defeatScreen;
        [SerializeField] private GameObject _victoryScreen;
        [SerializeField] private GameObject _splashScreen;

        public void ShowSplashScreen()
        {
            _splashScreen.SetActive(true);
            _darkEffect.SetActive(true);
        }
        
        public void ShowVictoryScreen()
        {
            _victoryScreen.SetActive(true);
            _darkEffect.SetActive(true);

        }
        
        public void ShowDefeatScreen()
        {
            _defeatScreen.SetActive(true);
            _darkEffect.SetActive(true);
        }

        public void HideAll()
        {
            _splashScreen.SetActive(false);
            _victoryScreen.SetActive(false);
            _defeatScreen.SetActive(false);
            _darkEffect.SetActive(false);

        }
    }
}