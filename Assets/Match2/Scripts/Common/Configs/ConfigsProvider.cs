using Match2.Scripts.Common.Audio;
using UnityEngine;

namespace Match2.Scripts.Common.Configs
{
    public class ConfigsProvider : MonoBehaviour
    {
        [SerializeField]
        private SoundsConfig soundsConfig;

        public SoundsConfig SoundsConfig => soundsConfig;
    }
}
