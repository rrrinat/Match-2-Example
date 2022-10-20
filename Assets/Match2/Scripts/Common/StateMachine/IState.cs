namespace Match2.Scripts.Common.StateMachine
{
    public interface IState
    {
        void Enter();
        void Update();
        void Exit();
    }
}