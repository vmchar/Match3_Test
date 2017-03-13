using UnityEngine;

namespace Match3Test
{
    public class ColoredElementModel : BaseElementModel
    {
        /// <summary>
        /// Colored element have no matching stategy. 
        /// GameController cosideres it as default one and uses it's default matching strategy
        /// </summary>
        /// <param name="type">Type.</param>
        /// <param name="normalSprite">Normal sprite.</param>
        /// <param name="selectedSprite">Selected sprite.</param>
        public ColoredElementModel (ElementType type, Sprite normalSprite, Sprite selectedSprite) 
            : base (type, normalSprite, selectedSprite)
        {
            _machingTypes.Add (type);
        }
    }

}
