using UnityEngine;

namespace ArmorVehicle
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] private Weapon _weapon;

        public void Initialize(IInputHandler inputHandler, float damage, float attackCooldown)
        {
            _weapon.Initialize(inputHandler, damage, attackCooldown);
            Enable(false);
        }

        public void Enable(bool isActive)
        {
            _weapon.Enable(isActive);
        }
    }
}