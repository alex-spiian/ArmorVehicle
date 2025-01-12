namespace ArmorVehicle
{
    public interface IState : IInitializable
    {
        public void OnEnter();
    }
}