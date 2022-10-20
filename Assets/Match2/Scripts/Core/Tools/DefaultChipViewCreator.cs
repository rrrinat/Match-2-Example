using Match2.Scripts.Common.Tools;
using Match2.Scripts.Core.Level;
using UnityEngine;

namespace Match2.Scripts.Core.Tools
{
    public class DefaultChipViewCreator
    {
        private SpriteDispenser spriteDispenser;
        
        public DefaultChipViewCreator()
        {
            spriteDispenser = new SpriteDispenser();
        }
        
        public ChipView Create(ChipEntity entity)
        {
            var context = entity.Context;
            var parent = entity.Parent;
            var holder = parent.View.transform;
            var sprite = spriteDispenser.GetSprite(entity.Type, entity.Color);
            
            var view = GameObject.Instantiate(context.Prefab);
            view.transform.SetParent(holder, false);
            view.Initialize(sprite);

            return view;
        }
    }
}
