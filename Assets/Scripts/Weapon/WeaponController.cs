using UnityEngine;

namespace ArmorVehicle
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] private Weapon _weapon;

        public void Initialize(IInputHandler inputHandler)
        {
            _weapon.Initialize(inputHandler);
            Enable(false);
        }

        public void Enable(bool isActive)
        {
            _weapon.Enable(isActive);
        }
    }
}