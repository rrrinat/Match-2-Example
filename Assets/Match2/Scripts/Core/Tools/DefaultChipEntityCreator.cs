using Match2.Scripts.Common.Audio;
using Match2.Scripts.Core.Level;
using Match2.Scripts.Core.VFX;
using Match2.Scripts.Static;
using UnityEngine;

namespace Match2.Scripts.Core.Tools
{
    public class DefaultChipEntityCreator
    {
        private DefaultChipEntityCreatorContext context;
      
        private LevelChannel levelChannel;
        private CubeDestroyPoolEntity cubeDestroyPool;
        private AudioSourcePoolEntity audioSourcePool;
        private DefaultChipViewCreator chipViewCreator;        
        
        public DefaultChipEntityCreator(DefaultChipEntityCreatorContext context)
        {
            this.context = context;

            this.levelChannel = context.LevelChannel;
            this.cubeDestroyPool = context.CubeDestroyPool;
            this.audioSourcePool = context.AudioSourcePool;
            this.chipViewCreator = context.ChipViewCreator;
        }

        public ChipEntity Create(CellEntity cellEntity)
        {
            var currentChipData = levelChannel.RandomChipData();
            //var currentChipData = levelChannel.TestChipData(cellEntity.Coord);
                    
            var chipEntityContext = new ChipEntityContext
            {
                Data = currentChipData,
                Prefab = Resources.Load<ChipView>($"Prefabs/Chips/Common"),
                ChipViewCreator = chipViewCreator,
                CubeDestroyPool = cubeDestroyPool,
                AudioSourcePool = audioSourcePool
            };
            var chipEntity = new ChipEntity(chipEntityContext);
            cellEntity.SetChild(chipEntity);
            chipEntity.CreateView();
            chipEntity.Sort();

            return chipEntity;
        }
    }
}