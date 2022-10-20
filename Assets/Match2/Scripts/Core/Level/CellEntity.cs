using Cubie.Framework;
using Match2.Scripts.Core.Enums;
using UnityEngine;

namespace Match2.Scripts.Core.Level
{
    public class CellEntity : BaseDisposable
    {
        private CellEntityContext context;
        private Vector2Int fieldSize => context.FieldSize;
        private int cellSize = 1;
        
        private CellState cellState;
        
        private CellView view;
        private ChipEntity child;

        public CellEntityContext Context => context;
        public CellType CellType => context.CellType;
        public CellState CellState => cellState;
        public bool HasChild => child != null;
        public ChipEntity Child => child;
        public CellView View => view;
        public bool IsDefault => CellType == CellType.Default;
        public Vector2Int Coord => context.Coord;
        public Vector3 Position => view.transform.position;

        public bool CanBeFalled
        {
            get
            {
                if (CellType != CellType.Default)
                {
                    return false;
                }

                if (!HasChild)
                {
                    return false;
                }
                
                if (Child.State != ChipState.Default)
                {
                    return false;
                }
                return true;
            }
        }
        
        public CellEntity(CellEntityContext context)
        {
            this.context = context;
            var position = CoordToLocalPosition(context.Coord);
            
            var cellViewCreator = context.CellViewCreator;
            this.view = cellViewCreator.Create(this, position, context.OnCellClick);
        }

        public void SetState(CellState cellState)
        {
            this.cellState = cellState;
        }
        
        public void SetChild(ChipEntity chipEntity)
        {
            if (child != null)
            {
                Debug.LogError("Can not create chip in non empty cell");
                return;
            }
            
            child = chipEntity;
            
            chipEntity.SetParent(this);
        }

        public void ReleaseChild()
        {
            if (child == null)
            {
                return;
            }

            child = null;
            cellState = CellState.Default;           
        }
        public bool IsInteractable()
        {
            if (!IsDefault)
            {
                return false;
            }

            if (CellState == CellState.Blocked)
            {
                return false;
            }          
            
            if (!HasChild)
            {
                return false;
            }
            
            return true;
        }
        
        public bool IsMatched(CellEntity otherEntity)
        {
            return child.IsMatched(otherEntity.Child);
        }         
        
        private Vector3 CoordToLocalPosition(Vector2Int coord)
        {
            var x = cellSize * (coord.x - 0.5f * (fieldSize.x - 1));
            var y = cellSize * (coord.y - 0.5f * (fieldSize.y - 1));
        
            return new Vector3(x, y);
        }      
    }
}
