namespace Match2.Scripts.Common.Extensions
{
    public static class GameExtensions
    {
        private static int nextPoolId = 0;
        
        public static int NextPoolId
        {
            get { return nextPoolId++; }
        }
    }
}
