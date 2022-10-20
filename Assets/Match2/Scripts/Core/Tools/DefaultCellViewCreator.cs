using Cubie.Reactive;
using Match2.Scripts.Core.Level;
using UnityEngine;

namespace Match2.Scripts.Core.Tools
{
    public class DefaultCellViewCreator
    {
        public CellView Create(CellEntity entity, Vector3 position, ReactiveEvent<Vector2Int> onCellClick)
        {
            var context = entity.Context;
            var cellType = entity.CellType;
            
            var holder = context.Holder;
            var coord = context.Coord;

            var view = GameObject.Instantiate(context.Prefab, position, Quaternion.identity);
            view.gameObject.name = $"{cellType}Cell [{coord.x}, {coord.y}]";
            view.transform.SetParent(holder, false);

            var cellViewContext = new CellViewContext()
            {
                Coord = coord,
                CellType = cellType,
                OnCellClick = onCellClick
            };
            view.SetContext(cellViewContext);

            return view;
        }
    }
}
