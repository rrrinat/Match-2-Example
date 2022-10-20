using Match2.Scripts.Common.Audio;
using Match2.Scripts.Common.Configs;
using Match2.Scripts.Static;

namespace Match2.Scripts.Core.Level
{
    public struct LevelEntityContext
    {
        public LevelChannel LevelChannel;
        public AudioSourcePoolEntity AudioSourcePool;
        public ConfigsProvider ConfigsProvider;
    }
}
