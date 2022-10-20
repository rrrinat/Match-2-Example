using Cubie.Reactive;
using Match2.Scripts.Core.Enums;
using Match2.Scripts.Core.Tools;
using UniRx;
using UnityEngine;

namespace Match2.Scripts.Core.Level
{
    public struct CellEntityContext
    {
        public CellType CellType;
        public Transform Holder;
        public CellView Prefab;
        public FieldEntity Field;
        public Vector2Int Coord;
        public Vector2Int FieldSize;
        
        public DefaultCellViewCreator CellViewCreator;
        
        public ReactiveEvent<LevelInfo> OnLevelCreate;
        public ReactiveEvent<Vector2Int> OnCellClick;
    }
}
