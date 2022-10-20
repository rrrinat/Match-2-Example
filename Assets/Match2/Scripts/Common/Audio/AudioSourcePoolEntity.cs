using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cubie.Framework;
using UnityEngine;

namespace Match2.Scripts.Common.Audio
{
    public class AudioSourcePoolEntity : BaseDisposable
    {
        private SoundsConfig soundsConfig;
        
        private int initialInstanceCount = 20;
        private Dictionary<AudioType, AudioSourcePool> pools;     
        
        public AudioSourcePoolEntity(SoundsConfig soundsConfig)
        {
            this.soundsConfig = soundsConfig;
            
            pools = new Dictionary<AudioType, AudioSourcePool>();
            
            var managerHost = new GameObject("AudioSourcePool");

            var audioTypes = Enum.GetValues(typeof(AudioType));
            
            for (int i = 0; i < audioTypes.Length; i++)
            {
                var audioType = (AudioType)audioTypes.GetValue(i);

                var host = new GameObject(audioType.ToString());
                host.transform.SetParent(managerHost.transform);
                
                var prefab = Resources.Load<GameObject>($"Prefabs/Audio/{audioType.ToString()}");
                pools.Add(audioType, new AudioSourcePool(prefab, host, initialInstanceCount));
            }            
        }
        
        public bool SpawnPlayer(AudioType audioType, Vector3 position)
        {
            if (!pools.ContainsKey(audioType))
            {
                return false;
            }

            InternalSpawnAudioPlayer(audioType, position);
            return true;
        }

        public async void SpawnPlayerAndReturn(AudioType audioType, Vector3 position, int duration)
        {
            if (!pools.ContainsKey(audioType))
            {
                return;
            }

            var player = InternalSpawnAudioPlayer(audioType, position);
            player.Play();
            await WaitAndReturn(player, duration);
        }
        
        public async void PlayClip(SoundType soundType, Vector3 position, int duration, float volume = 1f)
        {
            await PlayClipInternal(soundType, position, duration, volume);
        }        

        public async void PlayClip(SoundType soundType, Vector3 position, int duration)
        {
            await PlayClipInternal(soundType, position, duration);
        }           
        
        public async Task PlayClipInternal(SoundType soundType, Vector3 position, int duration, float volume = 1f)
        {
            if (!pools.ContainsKey(AudioType.Default))
            {
                return;
            }

            var sound = soundsConfig.GetSound(soundType);
            if (!sound)
            {
                return;
            }
            
            var player = InternalSpawnAudioPlayer(AudioType.Default, position);
            player.Play(sound, volume);
            await WaitAndReturn(player, duration);
        }    
        
        private async Task WaitAndReturn(AudioSourcePlayer player, int duration)
        {
            await Task.Delay(duration);
            if (player == null)
            {
                return;
            }
            player.Reset();
            
            pools[player.AudioType].Return(player);
        }
        
        private AudioSourcePlayer InternalSpawnAudioPlayer(AudioType audioType, Vector3 position)
        {
            var pool = pools[audioType];

            var player = pool.Get(position, Quaternion.identity);
            player.AudioType = audioType;

            return player;
        }
        
        public void Return(AudioSourcePlayer player)
        {
            pools[player.AudioType].Return(player);
        }        
    }
}