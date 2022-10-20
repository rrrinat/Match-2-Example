using System.Collections.Generic;

namespace Match2.Scripts.Core.Level
{
    public class Match : List<CellEntity>
    {
        public Match()
        {
            
        }

        public Match(Match match) : base(match)
        {
            
        }
        
        public Match(List<CellEntity> cellEntities) : base(cellEntities)
        {
            
        }       
    }
}
