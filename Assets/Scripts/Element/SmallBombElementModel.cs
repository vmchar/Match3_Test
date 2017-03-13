using UnityEngine;
using System;

namespace Match3Test
{
    public class SmallBombElementModel : BaseElementModel
    {
        public SmallBombElementModel (ElementType type, Sprite normalSprite, Sprite selectedSprite) 
            : base (type, normalSprite, selectedSprite)
        {
            foreach (ElementType possibleType in Enum.GetValues(typeof(ElementType)))
                { _machingTypes.Add (possibleType); }
            _matchingStrategy = new SmallBlastMatchingStrategy ();
        }
    }
}