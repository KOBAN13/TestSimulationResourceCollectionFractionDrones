using States;

namespace State
{
    public interface IState
    {
        DronStateMachine StateMachine { get; }
        void OnEnter();
        void OnExit();
        void OnUpdateBehaviour();
        void OnFixedUpdateBehaviour();
        bool TrySwapState();
    }
}