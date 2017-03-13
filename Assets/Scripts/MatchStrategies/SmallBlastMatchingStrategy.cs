using UnityEngine;
using System.Collections;

namespace Match3Test
{

    public class SmallBlastMatchingStrategy : BaseMathchingStategy
    {
        public override bool TryMatch (FieldController field, TileController firstTile, TileController secondTile)
        {
            //TODO: destroy all the Elements in +-1,+-1 radius
            return base.TryMatch (field, firstTile, secondTile);
        }
    }

}
