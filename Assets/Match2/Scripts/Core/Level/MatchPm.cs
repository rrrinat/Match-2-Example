using System.Collections.Generic;
using Cubie.Framework;
using Cubie.Reactive;
using Match2.Scripts.Core.Enums;
using UnityEngine;

namespace Match2.Scripts.Core.Level
{
    public class MatchPm : BaseDisposable
    {
        private MatchPmContext context;
        private FieldEntity fieldEntity;
        private CellEntity[,] cells;

        private ReactiveEvent<Vector2Int> onCellClick;
        private ReactiveEvent<HashSet<CellEntity>> onMatchFound;
        
        private int requiredCellsCount = 2;
        private Vector2Int fieldSize;
        
        private Vector2Int[] directions
        {
            get
            {
                return new Vector2Int[]
                {
                    Vector2Int.left,
                    Vector2Int.right,
                    Vector2Int.up,
                    Vector2Int.down
                };               
            }
        }

        public MatchPm(MatchPmContext context)
        {
            this.context = context;
            fieldEntity = context.FieldEntity;
            cells = fieldEntity.Cells;
            fieldSize.x = cells.GetLength(0);
            fieldSize.y = cells.GetLength(1);             
            
            onCellClick = context.OnCellClick;
            onMatchFound = context.OnMatchFound;
            
            AddUnsafe(onCellClick.SubscribeWithSkip(OnCellClicked));
        }
        
        private void OnCellClicked(Vector2Int coord)
        {
            var currentCell = cells[coord.x, coord.y];
            if (!currentCell.IsInteractable())
            {
                return;
            }
            var cellEntities = BFS(currentCell);

            if (cellEntities.Count >= requiredCellsCount)
            {
                onMatchFound.Notify(cellEntities);
            }
            else
            {
                currentCell.Child.Decline();
            }
        }
        
        public HashSet<CellEntity> BFS(CellEntity start)
        {
            var visited = new HashSet<CellEntity>();

            var queue = new Queue<CellEntity>();
            queue.Enqueue(start);
            while (queue.Count > 0)
            {
                var cellEntity = queue.Dequeue();

                if (visited.Contains(cellEntity))
                {
                    continue;
                }

                visited.Add(cellEntity);

                var neighbours = GetNeighbours(cellEntity);
                foreach (var neighbour in neighbours)
                {
                    if (!neighbour.IsInteractable())
                    {
                        continue;
                    }

                    if (neighbour.Child.Type != ObjectType.Default)
                    {
                        continue;
                    }
                    
                    if (!neighbour.IsMatched(start))
                    {
                        continue;
                    }
                    
                    if (!visited.Contains(neighbour))
                    {
                        queue.Enqueue(neighbour);
                    }
                }
            }

            return visited;
        }
        
        private List<CellEntity> GetNeighbours(CellEntity currentCellEntity)
        {
            var neighbours = new List<CellEntity>();
            foreach (var direction in directions)
            {
                var checkedCoord = currentCellEntity.Coord + direction;
                if (WithinBoundaries(checkedCoord))
                {
                    var checkedCellEntity = cells[checkedCoord.x, checkedCoord.y];
                    if (checkedCellEntity.IsDefault)
                    {
                        neighbours.Add(checkedCellEntity);
                    }
                }
            }

            return neighbours;
        }

        private bool WithinBoundaries(Vector2Int coord)
        {
            return coord.x >= 0 && coord.x < fieldSize.x &&
                   coord.y >= 0 && coord.y < fieldSize.y;
        }
    }
}
