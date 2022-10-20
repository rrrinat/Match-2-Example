using System.Collections.Generic;
using Match2.Scripts.Core.Enums;
using Match2.Scripts.Core.Level;
using UnityEngine;

namespace Match2.Scripts.Static
{
    public class LevelChannel
    {
        public List<LevelInfo> Levels { get; }
        public List<ChipData> ChipDatas { get; private set; }
        
        public ChipData[,] TestChipDatas { get; private set; }

        private int xSize = 5;
        private int ySize = 7;
        private int xObstacleIndex = 2;
        private int yObstacleIndex = 2;          
        
        public LevelChannel()
        {
            Levels = new List<LevelInfo>();
            
            CreateTestLevel();
            CreateTestChipDatas();
            
            CreateChipDatas();
        }
        
        private void CreateTestLevel()
        {
            var cellsData = new CellType[xSize, ySize];
            for (var x = 0; x < xSize; x++)
            {
                for (var y = 0; y < ySize; y++)
                {
                    cellsData[x, y] = GetCellType(x, y);
                }
            }

            var chipData = new ChipData
            {
                Type = ObjectType.Default,
                Color = ObjectColor.Red
            };
            var firstGoal = new GoalInfo
            {
                ChipData = chipData,
                Amount = 10
            };
            
            var goals = new List<GoalInfo> { firstGoal };
            var firstLevel = new LevelInfo
            {
                LevelIndex = 1,
                Goals = goals,
                CellsData = cellsData
            };

            Levels.Add(firstLevel);
        }

        private CellType GetCellType(int x, int y)
        {
            CellType cellType;
                    
            if (y == ySize - 1 )
            {
                cellType = CellType.SpawnPoint;
            }
            else if (x == xObstacleIndex && y == yObstacleIndex)
            {
                cellType = CellType.Obstacle;
            }
            else
            {
                cellType = CellType.Default;
            }

            return cellType;
        }
        
        private void CreateChipDatas()
        {
            ChipDatas = new List<ChipData>
            {
                new ChipData { Color = ObjectColor.Red, Type = ObjectType.Default},
                new ChipData { Color = ObjectColor.Blue, Type = ObjectType.Default},
                new ChipData { Color = ObjectColor.Green, Type = ObjectType.Default},
            };
        }

        public ChipData RandomChipData()
        {
            var count = ChipDatas.Count;
            var randomIndex = UnityEngine.Random.Range(0, count);
            
            return ChipDatas[randomIndex];
        }

        private void CreateTestChipDatas()
        {
            TestChipDatas = new ChipData[xSize, ySize];
            
            TestChipDatas[0, 0] = new ChipData { Color = ObjectColor.Blue, Type = ObjectType.Default };
            TestChipDatas[1, 0] = new ChipData { Color = ObjectColor.Green, Type = ObjectType.Default };
            TestChipDatas[2, 0] = new ChipData { Color = ObjectColor.Green, Type = ObjectType.Default };
            TestChipDatas[3, 0] = new ChipData { Color = ObjectColor.Red, Type = ObjectType.Default };
            TestChipDatas[4, 0] = new ChipData { Color = ObjectColor.Red, Type = ObjectType.Default };
            
            TestChipDatas[0, 1] = new ChipData { Color = ObjectColor.Yellow, Type = ObjectType.Default };
            TestChipDatas[1, 1] = new ChipData { Color = ObjectColor.Blue, Type = ObjectType.Default };
            TestChipDatas[2, 1] = new ChipData { Color = ObjectColor.Green, Type = ObjectType.Default };
            TestChipDatas[3, 1] = new ChipData { Color = ObjectColor.Orange, Type = ObjectType.Default };
            TestChipDatas[4, 1] = new ChipData { Color = ObjectColor.Purple, Type = ObjectType.Default };
            
            TestChipDatas[0, 2] = new ChipData { Color = ObjectColor.Purple, Type = ObjectType.Default };
            TestChipDatas[1, 2] = new ChipData { Color = ObjectColor.Orange, Type = ObjectType.Default };
            //TestChipDatas[2, 2] = new ChipData { Color = ObjectColor.Green, Type = ObjectType.Default };
            TestChipDatas[3, 2] = new ChipData { Color = ObjectColor.Blue, Type = ObjectType.Default };
            TestChipDatas[4, 2] = new ChipData { Color = ObjectColor.Yellow, Type = ObjectType.Default };
                        
            TestChipDatas[0, 3] = new ChipData { Color = ObjectColor.Green, Type = ObjectType.Default };
            TestChipDatas[1, 3] = new ChipData { Color = ObjectColor.Green, Type = ObjectType.Default };
            TestChipDatas[2, 3] = new ChipData { Color = ObjectColor.Green, Type = ObjectType.Default };
            TestChipDatas[3, 3] = new ChipData { Color = ObjectColor.Orange, Type = ObjectType.Default };
            TestChipDatas[4, 3] = new ChipData { Color = ObjectColor.Purple, Type = ObjectType.Default };
            
            TestChipDatas[0, 4] = new ChipData { Color = ObjectColor.Purple, Type = ObjectType.Default };
            TestChipDatas[1, 4] = new ChipData { Color = ObjectColor.Orange, Type = ObjectType.Default };
            TestChipDatas[2, 4] = new ChipData { Color = ObjectColor.Green, Type = ObjectType.Default };
            TestChipDatas[3, 4] = new ChipData { Color = ObjectColor.Green, Type = ObjectType.Default };
            TestChipDatas[4, 4] = new ChipData { Color = ObjectColor.Yellow, Type = ObjectType.Default };
                        
            TestChipDatas[0, 5] = new ChipData { Color = ObjectColor.Yellow, Type = ObjectType.Default };
            TestChipDatas[1, 5] = new ChipData { Color = ObjectColor.Blue, Type = ObjectType.Default };
            TestChipDatas[2, 5] = new ChipData { Color = ObjectColor.Green, Type = ObjectType.Default };
            TestChipDatas[3, 5] = new ChipData { Color = ObjectColor.Orange, Type = ObjectType.Default };
            TestChipDatas[4, 5] = new ChipData { Color = ObjectColor.Purple, Type = ObjectType.Default };
        }

        public ChipData TestChipData(int x, int y)
        {
            return TestChipDatas[x, y];
        }
        
        public ChipData TestChipData(Vector2Int coord)
        {
            return TestChipDatas[coord.x, coord.y];
        }
    }
}
