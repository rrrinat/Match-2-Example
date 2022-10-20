using Match2.Scripts.Core.Tools;

namespace Match2.Scripts.Core.Level.FieldStates
{
    public class FieldIdleState : FieldState
    {
        private FieldEntityContext context;
        public FieldIdleState(FieldStateMachine fieldStateMachine, FieldEntityContext context) : base(fieldStateMachine)
        {
            this.context = context;
        }

        public override void Enter()
        {
            
        }

        public override void Update()
        {
            
        }

        public override void Exit()
        {
            
        }
    }
}