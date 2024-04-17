namespace Health
{
    public interface IHealthHandler : IHealth
    {
        void TakeDamage(float damage);
    }
}