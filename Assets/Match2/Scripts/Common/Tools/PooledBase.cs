using Match2.Scripts.Common.Extensions;
using UnityEngine;

namespace Match2.Scripts.Common.Tools
{
    public class PooledBase : MonoBehaviour, IPooled
    {
        protected int poolId;

        public int Id => poolId;

        private void OnEnable()
        {
            poolId = GameExtensions.NextPoolId;
        }
    }
}