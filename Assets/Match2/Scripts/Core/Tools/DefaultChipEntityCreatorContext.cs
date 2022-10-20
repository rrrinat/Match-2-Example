using Match2.Scripts.Common.Audio;
using Match2.Scripts.Core.VFX;
using Match2.Scripts.Static;

namespace Match2.Scripts.Core.Tools
{
    public struct DefaultChipEntityCreatorContext
    {
        public LevelChannel LevelChannel;
        public CubeDestroyPoolEntity CubeDestroyPool;
        public AudioSourcePoolEntity AudioSourcePool;
        public DefaultChipViewCreator ChipViewCreator;
    }
}