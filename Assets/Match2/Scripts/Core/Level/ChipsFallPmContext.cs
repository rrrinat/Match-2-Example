using System.Collections.Generic;
using Match2.Scripts.Core.Tools;

namespace Match2.Scripts.Core.Level
{
    public struct ChipsFallPmContext
    {
        public FieldEntity FieldEntity;
        public DefaultChipEntityCreator ChipEntityCreator;
        public Dictionary<int, List<CellEntity>> Rows;
        public Dictionary<int, List<CellEntity>> Columns;
    }
}
