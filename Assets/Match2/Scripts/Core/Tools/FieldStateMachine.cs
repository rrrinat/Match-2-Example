using System;
using System.Collections.Generic;
using Match2.Scripts.Common.StateMachine;
using Match2.Scripts.Core.Level;
using Match2.Scripts.Core.Level.FieldStates;

namespace Match2.Scripts.Core.Tools
{
    public class FieldStateMachine : StateMachine
    {
        private readonly Dictionary<Type, FieldState> fieldStates = new Dictionary<Type, FieldState>();

        private FieldEntityContext context;
        
        public FieldStateMachine(FieldEntityContext context)
        {
            this.context = context;

            var fieldClearState = new FieldClearState(this, context);
            var fieldSpawnState = new FieldSpawnState(this, context);
            var fieldIdleState = new FieldIdleState(this, context);
            var fieldDestroyingMatchesState = new FieldDestroyingMatchesState(this, context);
            var fieldChipsFallingState = new FieldChipsFallingState(this, context);
            
            AddState(fieldClearState);
            AddState(fieldSpawnState);
            AddState(fieldIdleState);
            AddState(fieldDestroyingMatchesState);
            AddState(fieldChipsFallingState);

            Initialize(fieldIdleState);
        }

        private void AddState<T>(T fieldState) where T : FieldState
        {
            fieldStates[typeof(T)] = fieldState;
        }

        public void SetState<T>() where T : FieldState
        {
            if (fieldStates.TryGetValue(typeof(T), out var state))
            {
                SetState(state);
            }
        }
    }
}