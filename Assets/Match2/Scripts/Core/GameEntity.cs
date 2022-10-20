using Cubie.Framework;
using Match2.Scripts.Common.Audio;
using Match2.Scripts.Core.Level;
using Match2.Scripts.Static;

namespace Match2.Scripts.Core
{
    public class GameEntity : BaseDisposable
    {
        private GameEntitiyContext context;

        private LevelEntity levelEntity;
        private AudioSourcePoolEntity audioSourcePool;

        public GameEntity(GameEntitiyContext context)
        {
            this.context = context;

            audioSourcePool = context.AudioSourcePool;

            StartLevel();
        }

        private void StartLevel()
        {
            var levelChannel = new LevelChannel(); 
            var levelEntityContext = new LevelEntityContext
            {
                LevelChannel = levelChannel,
                AudioSourcePool = audioSourcePool,
                ConfigsProvider = context.ConfigsProvider
            };
            
            levelEntity = new LevelEntity(levelEntityContext);
            
            
        }
        
        protected override void OnDispose()
        {
            base.OnDispose();
            
            levelEntity?.Dispose();
        }        
    }
}
