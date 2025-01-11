namespace ArmorVehicle
{
    public interface IHealthHandler : IHealth
    {
        void TakeDamage(float damage);
    }
}