using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cubie.Reactive;
using Match2.Scripts.Common.Audio;
using Match2.Scripts.Core.Tools;
using UnityEngine;

namespace Match2.Scripts.Core.Level.FieldStates
{
    public class FieldDestroyingMatchesState : FieldState
    {
        private FieldEntityContext context;
        private ReactiveEvent<HashSet<CellEntity>> onMatchFound;

        private IDisposable onMatchFoundSubscribtion;
        
        private AudioSourcePoolEntity audioSourcePool;
        private MatchPm matchPm;       
        private ChipsFallPm chipsFallPm;       
        
        public FieldDestroyingMatchesState(FieldStateMachine fieldStateMachine, FieldEntityContext context) : base(fieldStateMachine)
        {
            this.context = context;
            this.onMatchFound = context.OnMatchFound;
            this.audioSourcePool = context.AudioSourcePool;
            this.matchPm = context.MatchPm;
            this.chipsFallPm = context.ChipsFallPm;
        }

        public override void Enter()
        {
            onMatchFoundSubscribtion = onMatchFound.SubscribeWithSkip(OnMatchFound);
        }

        public override void Update()
        {
            
        }

        public override void Exit()
        {
            onMatchFoundSubscribtion?.Dispose();
        }
        
        private async void OnMatchFound(HashSet<CellEntity> cellEntities)
        {
            await DestroyMatch(cellEntities);
            audioSourcePool.PlayClip(SoundType.Match, Vector3.zero, 1000);
            FieldStateMachine.SetState<FieldChipsFallingState>();
        }
        
        private async Task DestroyMatch(HashSet<CellEntity> cellEntities)
        {
            foreach (var cellEntity in cellEntities)
            {
                await Destroy(cellEntity);
            }
        }
        
        private async Task Destroy(CellEntity cellEntity)
        {
            if (!cellEntity.IsInteractable())
            {
                return;
            }
            audioSourcePool.PlayClip(SoundType.Destroy, Vector3.zero, 1000, 0.1f);
            await cellEntity.Child.Destroy();
        }
    }
}