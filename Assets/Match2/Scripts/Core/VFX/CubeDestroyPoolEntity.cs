using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cubie.Framework;
using Match2.Scripts.Core.Enums;
using UnityEngine;

namespace Match2.Scripts.Core.VFX
{
    public class CubeDestroyPoolEntity : BaseDisposable
    {
        private int initialInstanceCount = 20;
        private Dictionary<ObjectColor, CubeDestroyPool> pools;
        
        public CubeDestroyPoolEntity()
        {
            pools = new Dictionary<ObjectColor, CubeDestroyPool>();
            
            var managerHost = new GameObject("CubeDestroyPool");
            //managerHost.transform.SetParent(this.transform);

            var colors = Enum.GetValues(typeof(ObjectColor));
            
            for (int i = 0; i < 7; i++)
            {
                var color = (ObjectColor)colors.GetValue(i);
                if (color == ObjectColor.None)
                {
                    continue;
                }

                var host = new GameObject(color.ToString());
                host.transform.SetParent(managerHost.transform);
                
                var prefab = Resources.Load<GameObject>($"Prefabs/VFX/CubeDestroy/Cube_{color.ToString()}_Destroy");
                pools.Add(color, new CubeDestroyPool(prefab, host, initialInstanceCount));
            }            
        }
        
        public bool SpawnVFX(ObjectColor color, Vector3 position)
        {
            if (color == ObjectColor.None || !pools.ContainsKey(color))
            {
                return false;
            }

            InternalSpawnVFX(color, position);
            return true;
        }

        public async void SpawnVFXAndReturn(ObjectColor color, Vector3 position)
        {
            if (color == ObjectColor.None || !pools.ContainsKey(color))
            {
                return;
            }

            var vfx = InternalSpawnVFX(color, position);
            await WaitAndReturn(vfx);
        }

        private async Task WaitAndReturn(CubeDestroyVFX vfx)
        {
            await Task.Delay(2000);
            if (vfx == null)
            {
                return;
            }
            
            pools[vfx.Color].Return(vfx);
        }
        
        private CubeDestroyVFX InternalSpawnVFX(ObjectColor color, Vector3 position)
        {
            var pool = pools[color];

            var vfx = pool.Get(position, Quaternion.identity);
            vfx.Color = color;

            return vfx;
        }
        
        public void Return(CubeDestroyVFX vfx)
        {
            pools[vfx.Color].Return(vfx);
        }    
    }
}
