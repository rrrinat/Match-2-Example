using Cubie.Framework;
using Match2.Scripts.Common.Audio;
using Match2.Scripts.Core;

namespace Match2.Scripts
{
    public class RootEntity : BaseDisposable
    {
        private readonly RootEntityContext context;
        private GameEntity gameEntity;
        
        public RootEntity(RootEntityContext context)
        {
            this.context = context;

            CreateGameEntity();
        }

        private void CreateGameEntity()
        {
            var soundsConfig = context.ConfigsProvider.SoundsConfig;
            var audioSourcePool = new AudioSourcePoolEntity(soundsConfig);
            
            var gameEntityContext = new GameEntitiyContext()
            {
                AudioSourcePool = audioSourcePool,
                ConfigsProvider = context.ConfigsProvider
            };
            gameEntity = new GameEntity(gameEntityContext);

            AddUnsafe(gameEntity);
        }
    }
}
