using System.Collections.Generic;
using System.Linq;
using Cubie.Framework;
using Cubie.Reactive;
using Match2.Scripts.Common.Audio;
using Match2.Scripts.Core.Enums;
using Match2.Scripts.Core.Level.FieldStates;
using Match2.Scripts.Core.Tools;
using Match2.Scripts.Core.VFX;
using UnityEngine;

namespace Match2.Scripts.Core.Level
{
    public class FieldEntity : BaseDisposable
    {
        private FieldEntityContext context;

        private DefaultChipEntityCreator chipEntityCreator;
        private DefaultCellViewCreator cellViewCreator;
        private DefaultChipViewCreator chipViewCreator;

        private FieldStateMachine fieldStateMachine;
        
        private Vector2Int fieldSize;
        private CellEntity[,] cells;

        private Dictionary<int, List<CellEntity>> rows;
        private Dictionary<int, List<CellEntity>> columns;

        private MatchPm matchPm;
        private ChipsFallPm chipsFallPm;

        private CubeDestroyPoolEntity cubeDestroyPool;
        private AudioSourcePoolEntity audioSourcePool;
        
        private readonly ReactiveEvent<Vector2Int> onCellClick = new ReactiveEvent<Vector2Int>();
        private ReactiveEvent<HashSet<CellEntity>> onMatchFound;

        public CellEntity[,] Cells => cells;

        public FieldEntity(FieldEntityContext context)
        {
            this.context = context;

            audioSourcePool = context.AudioSourcePool;
            onMatchFound = context.OnMatchFound;

            rows = new Dictionary<int, List<CellEntity>>();
            columns = new Dictionary<int, List<CellEntity>>();
            
            CreateTools();
            CreateField();
            InitializeRows();
            InitializeColumns();
            CreateChips();
            CreatePms();

            context.MatchPm = matchPm;
            context.ChipsFallPm = chipsFallPm;
            
            AddUnsafe(onMatchFound.SubscribeWithSkip(OnMatchFound));
            
            fieldStateMachine = new FieldStateMachine(context);
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            
            cubeDestroyPool?.Dispose();
            matchPm?.Dispose();
            chipsFallPm?.Dispose();
        }

        private void CreateTools()
        {
            cellViewCreator = new DefaultCellViewCreator();
            chipViewCreator = new DefaultChipViewCreator();
            cubeDestroyPool = new CubeDestroyPoolEntity();

            var defaultChipEntityCreatorContext = new DefaultChipEntityCreatorContext
            {
                LevelChannel = context.LevelChannel,
                ChipViewCreator = chipViewCreator,
                CubeDestroyPool = cubeDestroyPool,
                AudioSourcePool = audioSourcePool
            };

            chipEntityCreator = new DefaultChipEntityCreator(defaultChipEntityCreatorContext);
        }
        
        private void CreateField()
        {
            var fieldView = new GameObject("FieldView");
            var onLevelCreate = new ReactiveEvent<LevelInfo>();
            
            var levelChannel = context.LevelChannel;
            var currentLevel = levelChannel.Levels.FirstOrDefault();
            if (currentLevel == null)
            {
                return;
            }
            
            var cellsData = currentLevel.CellsData;

            fieldSize.x = cellsData.GetLength(0);
            fieldSize.y = cellsData.GetLength(1);
            
            cells = new CellEntity[fieldSize.x, fieldSize.y];
            for (int y = 0; y < fieldSize.y; y++)
            {
                for (int x = 0; x < fieldSize.x; x++)
                {
                    var cellType = cellsData[x, y];
                    var cellEntityContext = new CellEntityContext
                    {
                        CellType = cellType,
                        Holder = fieldView.transform,
                        Field = this,
                        Coord = new Vector2Int(x, y),
                        FieldSize = fieldSize,
                        Prefab = Resources.Load<CellView>($"Prefabs/Cells/{cellType.ToString()}"),
                        
                        CellViewCreator = cellViewCreator,
                        OnLevelCreate = onLevelCreate,
                        OnCellClick = onCellClick
                    };            
                    var cellEntity = new CellEntity(cellEntityContext);
                    cells[x, y] = cellEntity;
                }
            }
            onLevelCreate.Notify(currentLevel);
        }

        private void InitializeRows()
        {
            var xLength = cells.GetLength(0);
            var yLength = cells.GetLength(1);            
            
            //rows
            for (int y = 0; y < yLength; y++)
            {
                var currentRow = new List<CellEntity>();
                for (int x = 0; x < xLength; x++)
                {
                    currentRow.Add(cells[x, y]);
                }
                rows.Add(y, currentRow);
            }
        }

        private void InitializeColumns()
        {
            var xLength = cells.GetLength(0);
            var yLength = cells.GetLength(1);               
            //columns
            for (int x = 0; x < xLength; x++)
            {
                var currentColumn = new List<CellEntity>();
                for (int y = 0; y < yLength; y++)
                {
                    currentColumn.Add(cells[x, y]);
                }

                columns.Add(x, currentColumn);
            }          
        }
        
        private void CreateChips()
        {
            for (int y = 0; y < fieldSize.y; y++)
            {
                for (int x = 0; x < fieldSize.x; x++)
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

                    var chipEntity = chipEntityCreator.Create(currentCell);
                }
            }          
        }
        
        private void CreatePms()
        {
            var matchPmContext = new MatchPmContext
            {
                FieldEntity = this,
                OnCellClick = onCellClick,
                OnMatchFound = onMatchFound
            };
            matchPm = new MatchPm(matchPmContext);
            
            var chipsFallPmContext = new ChipsFallPmContext
            {
                FieldEntity = this,
                ChipEntityCreator = chipEntityCreator,
                Rows = rows,
                Columns = columns
            };
            chipsFallPm = new ChipsFallPm(chipsFallPmContext);
        }

        private void OnCellClicked(Vector2Int coord)
        {

        }

        private void OnMatchFound(HashSet<CellEntity> cellEntities)
        {
           fieldStateMachine.SetState<FieldDestroyingMatchesState>(); 
        }
    }
}