using System.Collections.Generic;
using Cubie.Framework;
using Cubie.Reactive;

namespace Match2.Scripts.Core.Level
{
    public class LevelEntity : BaseDisposable
    {
        private LevelEntityContext context;

        private FieldEntity field;

        public LevelEntity(LevelEntityContext context)
        {
            this.context = context;

            CreateField();
        }

        private void CreateField()
        {
            var onMatchFound = new ReactiveEvent<HashSet<CellEntity>>();

            var fieldEntityContext = new FieldEntityContext
            {
                LevelChannel = context.LevelChannel,
                AudioSourcePool = context.AudioSourcePool,
                ConfigsProvider = context.ConfigsProvider,
                OnMatchFound = onMatchFound
            };
            field = new FieldEntity(fieldEntityContext);

            AddUnsafe(field);
        }
    }
}
