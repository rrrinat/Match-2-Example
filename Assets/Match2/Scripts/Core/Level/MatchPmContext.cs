using System.Collections.Generic;
using Cubie.Reactive;
using UnityEngine;

namespace Match2.Scripts.Core.Level
{
    public struct MatchPmContext
    {
        public FieldEntity FieldEntity;
        public ReactiveEvent<Vector2Int> OnCellClick;
        public ReactiveEvent<HashSet<CellEntity>> OnMatchFound;
    }
}
