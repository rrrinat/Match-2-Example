using Cubie.Reactive;
using Match2.Scripts.Core.Enums;
using UniRx;
using UnityEngine;

namespace Match2.Scripts.Core.Level
{
    public struct CellViewContext
    {
        public CellType CellType;
        public Vector2Int Coord;
        public ReactiveEvent<Vector2Int> OnCellClick;
    }
}
