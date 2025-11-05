namespace States.Interfaces
{
    public interface IState
    {
        DronStateMachine StateMachine { get; }
        string StateName { get; }
        void OnEnter();
        void OnExit();
        void OnUpdateBehaviour();
        void OnFixedUpdateBehaviour();
        bool TrySwapState();
    }
}