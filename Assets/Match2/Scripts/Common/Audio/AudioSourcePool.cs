using Match2.Scripts.Common.Tools;
using UnityEngine;

namespace Match2.Scripts.Common.Audio
{
    public class AudioSourcePool : PoolBase<AudioSourcePlayer>
    {
        public AudioSourcePool(GameObject prefab, GameObject host, int initialInstanceCount) : base(prefab, host, initialInstanceCount)
        {
            
        }
    }
}
