using System.Collections.Generic;
using UnityEngine;

namespace Match2.Scripts.Common.Audio
{
    [CreateAssetMenu(menuName = "Match2/Audio/Sounds Config")]
    public class SoundsConfig : ScriptableObject
    {
        [SerializeField]
        private List<SoundContainer> sounds;

        public AudioClip GetSound(SoundType soundType)
        {
            var found = sounds.Find(c => c.SoundType == soundType);

            if (found == null)
            {
                return null;
            }
            
            return found.AudioClip;
        }
    }
}
