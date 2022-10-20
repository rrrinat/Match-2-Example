using System.Collections.Generic;
using Cubie.Reactive;
using Match2.Scripts.Common.Audio;
using Match2.Scripts.Common.Configs;
using Match2.Scripts.Static;

namespace Match2.Scripts.Core.Level
{
    public struct FieldEntityContext
    {
        public LevelChannel LevelChannel;
        public AudioSourcePoolEntity AudioSourcePool;
        public ConfigsProvider ConfigsProvider;

        public MatchPm MatchPm;
        public ChipsFallPm ChipsFallPm;

        public ReactiveEvent<HashSet<CellEntity>> OnMatchFound;
    }
}
