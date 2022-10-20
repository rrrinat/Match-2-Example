using System.Threading.Tasks;
using Cubie.Framework;
using DG.Tweening;
using Match2.Scripts.Common.Audio;
using Match2.Scripts.Core.Enums;
using UnityEngine;

namespace Match2.Scripts.Core.Level
{
    public class ChipEntity : BaseDisposable
    {
        private ChipEntityContext context;

        private ChipView view;
        private CellEntity parent;
        private CellEntity target;
        private ChipState state;

        private AudioSourcePoolEntity audioSourcePool;
        
        public ChipEntityContext Context => context;
        public ChipData Data => context.Data;
        public ObjectType Type => Data.Type;
        public ObjectColor Color => Data.Color;
        public ChipState State => state;
        
        public CellEntity Parent => parent;
        public CellEntity Target => target;
        
        public ChipEntity(ChipEntityContext context)
        {
            this.context = context;
            this.audioSourcePool = context.AudioSourcePool;

            state = ChipState.Default;
        }
        
        /// <summary>
        /// Вызываем только из функции CellEntity.SetChild()
        /// </summary>
        /// <param name="cellEntity"></param>
        public void SetParent(CellEntity cellEntity)
        {
            this.parent = cellEntity;
        }
        
        public void SetTarget(CellEntity cellEntity)
        {
            this.target = cellEntity;

            cellEntity.SetState(CellState.Blocked);
            this.state = ChipState.ReadyToFall;
        }

        public async Task Fall()
        {
            await FallToInternal(Target);
            ResetCellState(Target);
        }
        
        private async Task FallToInternal(CellEntity toCell)
        {
            this.state = ChipState.FallDown;
            var moveTo = view.MoveTo(toCell.Position, 0.1f);
            
            await moveTo.AsyncWaitForCompletion();
        }
        
        private void ResetCellState(CellEntity toCell)
        {
            toCell.SetChild(this);
            toCell.SetState(CellState.Default);
            state = ChipState.Default;
            target = null;   
            Sort();
        }       
        
        public void CreateView()
        {
            var chipViewCreator = context.ChipViewCreator;
            this.view = chipViewCreator.Create(this);          
        }

        public void Decline()
        {
            this.view.Decline();
            audioSourcePool.PlayClip(SoundType.Select, view.transform.position, 1000);
        }
        
        public bool IsMatched(ChipEntity chipEntity)
        {
            return chipEntity.Type == Type && chipEntity.Color == Color;
        }

        public void Sort()
        {
            var coord = parent.Coord;
            
            int sortingOrder = (100 - coord.y * 5);
            SetSortingOrder(sortingOrder);
        }
        
        private void SetSortingOrder(int sortingOrder)
        {
            view.SetSortingOrder(sortingOrder);
        }

        public async Task Destroy()
        {
            if (view == null)
            {
                return;
            }

            view.Hide();
            
            await DestroyInternal();
        }

        private async Task DestroyInternal()
        {
            var cubeDestroyPool = context.CubeDestroyPool;
            cubeDestroyPool.SpawnVFXAndReturn(Color, view.transform.position);
            GameObject.Destroy(view.gameObject);

            await Task.Delay(30);
            
            parent.ReleaseChild();
            Dispose();           
        }
    }
}
