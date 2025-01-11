using UnityEngine;

namespace ArmorVehicle
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] private Weapon _weapon;

        public void Initialize(IInputHandler inputHandler)
        {
            _weapon.Initialize(inputHandler);
        }
    }
}