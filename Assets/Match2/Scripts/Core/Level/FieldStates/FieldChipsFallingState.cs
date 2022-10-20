using Match2.Scripts.Common.Audio;
using Match2.Scripts.Core.Tools;
using UnityEngine;

namespace Match2.Scripts.Core.Level.FieldStates
{
    public class FieldChipsFallingState : FieldState
    {
        private FieldEntityContext context;
        
        private AudioSourcePoolEntity audioSourcePool;
        private ChipsFallPm chipsFallPm;          
        
        public FieldChipsFallingState(FieldStateMachine fieldStateMachine, FieldEntityContext context) : base(fieldStateMachine)
        {
            this.context = context;
            
            this.audioSourcePool = context.AudioSourcePool;
            this.chipsFallPm = context.ChipsFallPm;
        }

        public override async void Enter()
        {
            await chipsFallPm.Fall();
            audioSourcePool.PlayClip(SoundType.Falldown, Vector3.zero, 1000);
            
            FieldStateMachine.SetState<FieldIdleState>();
        }

        public override void Update()
        {
            
        }

        public override void Exit()
        {
            
        }
    }
}