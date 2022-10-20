using Match2.Scripts.Core.Tools;

namespace Match2.Scripts.Core.Level.FieldStates
{
    public class FieldSpawnState : FieldState
    {
        private FieldEntityContext context;
        
        public FieldSpawnState(FieldStateMachine fieldStateMachine, FieldEntityContext context) : base(fieldStateMachine)
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