using System.Collections.Generic;
using System.Threading.Tasks;
using Cubie.Framework;
using Match2.Scripts.Core.Enums;
using Match2.Scripts.Core.Tools;

namespace Match2.Scripts.Core.Level
{
    public class ChipsFallPm : BaseDisposable
    {
        private ChipsFallPmContext context;
        private FieldEntity fieldEntity;
        private CellEntity[,] cells;

        private DefaultChipEntityCreator chipEntityCreator;
        
        private Dictionary<int, List<CellEntity>> rows;
        private Dictionary<int, List<CellEntity>> columns;
        
        private Queue<List<ChipEntity>> selectedChipsQueue;

        public ChipsFallPm(ChipsFallPmContext context)
        {
            this.context = context;
            fieldEntity = context.FieldEntity;
            cells = fieldEntity.Cells;

            chipEntityCreator = context.ChipEntityCreator;
            
            rows = context.Rows;
            columns = context.Columns;

            selectedChipsQueue = new Queue<List<ChipEntity>>();
        }

        public async Task Fall()
        {
            Select(cells);

            if (selectedChipsQueue.Count > 1)
            {
                return;
            }

            if (selectedChipsQueue.Count == 0)
            {
                return;
            }           
            
            var processedChips = selectedChipsQueue.Dequeue();
            foreach (var currentChip in processedChips)
            {
                currentChip.Fall();
            }
        }
        
        private void Select(CellEntity[,] cells)
        {
            var xSize = cells.GetLength(0);
            var ySize = cells.GetLength(1);
            
            var selectedChips = new List<ChipEntity>();
            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    var currentCell = cells[x, y];
                    if (currentCell.CellType != CellType.Default)
                    {
                        continue;
                    }
                    
                    if (currentCell.CellState == CellState.Blocked)
                    {
                        continue;
                    }

                    if (currentCell.HasChild)
                    {
                        continue;
                    }
                    //so... currentCell is empty
                    
                    var fromCell = GetSuitableCellUpwards(currentCell);
                    if (fromCell == null)
                    {
                        continue;
                    }
                    var chipEntity = fromCell.Child;
                    
                    fromCell.ReleaseChild();
                    chipEntity.SetTarget(currentCell);
                    
                    selectedChips.Add(chipEntity);
                }
            }
            if (selectedChips.Count == 0)
            {
                return;
            }
            selectedChipsQueue.Enqueue(selectedChips);
        }
        
        private CellEntity GetSuitableCellUpwards(CellEntity toCell)
        {
            var xSize = cells.GetLength(0);
            var ySize = cells.GetLength(1);
            
            var coord = toCell.Coord;

            for (int y = coord.y + 1; y < ySize; y++)
            {
                var fromCell = cells[coord.x, y];
                if (fromCell.CellType == CellType.Obstacle)
                {
                    continue;
                }

                if (fromCell.CellState == CellState.Blocked)
                {
                    continue;
                }

                if (fromCell.CanBeFalled)
                {
                    return fromCell;
                }

                if (fromCell.CellType == CellType.SpawnPoint)
                {
                    var chipEntity = chipEntityCreator.Create(fromCell);
                    
                    return fromCell;
                }
            }

            return null;
        }
    }
}