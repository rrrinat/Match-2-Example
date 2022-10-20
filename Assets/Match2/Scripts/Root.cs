using Cubie.Framework;

namespace Match2.Scripts
{
    public class Root : BaseDisposable
    {
        private readonly RootContext context;
        private RootEntity rootEntity;        
        
        private Root(RootContext context)
        {
            this.context = context;
            
            CreateRootEntity();
        }      
        
        #region защита от дурака
        private static Root _instance;
        
        public static Root CreateRoot(RootContext context)
        {
            if (RootExists)
            {
                return null;
            }
            _instance = new Root(context);
            return _instance;
        }

        public static bool RootExists
            => _instance != null;

        public void RecreatRootEntity()
        {
            rootEntity?.Dispose();
            CreateRootEntity();
        }

        protected override void OnDispose()
        {
            _instance = null;
        }
        #endregion        
        
        private void CreateRootEntity()
        {
            var rootEntityContext = new RootEntityContext
            {
                InputController = context.InputController,
                ConfigsProvider = context.ConfigsProvider
            };
            rootEntity = new RootEntity(rootEntityContext);
            
            AddUnsafe(rootEntity);
        }
    }
}
