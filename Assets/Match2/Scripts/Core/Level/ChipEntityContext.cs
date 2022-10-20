using Match2.Scripts.Common.Audio;
using Match2.Scripts.Core.Tools;
using Match2.Scripts.Core.VFX;

namespace Match2.Scripts.Core.Level
{
    public struct ChipEntityContext
    {
        public ChipData Data;
        public ChipView Prefab;
        public DefaultChipViewCreator ChipViewCreator;
        public CubeDestroyPoolEntity CubeDestroyPool;
        public AudioSourcePoolEntity AudioSourcePool;
    }
}
