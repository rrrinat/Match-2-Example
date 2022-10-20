using Match2.Scripts.Common.Tools;
using UnityEngine;

namespace Match2.Scripts.Core.VFX
{
    public class CubeDestroyPool : PoolBase<CubeDestroyVFX>
    {
        public CubeDestroyPool(GameObject prefab, GameObject host, int initialInstanceCount) : base(prefab, host, initialInstanceCount)
        {
            
        }
    }
}
