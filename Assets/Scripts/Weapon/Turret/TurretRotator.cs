using UnityEngine;

namespace ArmorVehicle
{
    public class TurretRotator : MonoBehaviour
    {
        [SerializeField]
        private float _speed;

        public void Rotate(float mouseX)
        {
            Vector3 rotation = new Vector3(0f, mouseX, 0f);
            transform.Rotate(rotation * _speed);
        }
    }
}