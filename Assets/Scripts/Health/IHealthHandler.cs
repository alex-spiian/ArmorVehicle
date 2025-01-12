namespace ArmorVehicle
{
    public interface IHealthHandler : IHealth
    {
        public void TakeDamage(float damage);
    }
}