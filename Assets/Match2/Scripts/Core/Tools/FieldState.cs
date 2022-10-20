using Match2.Scripts.Common.StateMachine;

namespace Match2.Scripts.Core.Tools
{
    public abstract class FieldState : IState
    {
        protected readonly FieldStateMachine FieldStateMachine;

        protected FieldState(FieldStateMachine fieldStateMachine)
        {
            FieldStateMachine = fieldStateMachine;
        }

        public abstract void Enter();
        public abstract void Update();
        public abstract void Exit();
    }
}