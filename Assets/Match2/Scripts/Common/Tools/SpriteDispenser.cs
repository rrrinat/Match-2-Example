using Match2.Scripts.Core.Enums;
using UnityEngine;

namespace Match2.Scripts.Common.Tools
{
    public class SpriteDispenser
    {
        public Sprite GetSprite(ObjectType objectType, ObjectColor objectColor)
        {
            return Resources.Load<Sprite>($"Textures/Cubes/cube_{objectColor}_{objectType}");
        }
    }
}
