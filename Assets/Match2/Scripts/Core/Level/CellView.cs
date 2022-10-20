using UnityEngine;

namespace Match2.Scripts.Core.Level
{
    public class CellView : MonoBehaviour
    {
        private CellViewContext context;
        public Vector2Int Coord => context.Coord;

        public void SetContext(CellViewContext context)
        {
            this.context = context;
        }

        public void OnCellClicked()
        {
            context.OnCellClick.Notify(Coord);
        }
    }
}
