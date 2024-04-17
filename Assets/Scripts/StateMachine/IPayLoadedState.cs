public interface IPayLoadedState<TPayload> : IInitializable
{
    void OnEnter(TPayload payload);
}