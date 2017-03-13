
namespace Match3Test
{
    public abstract class BaseMathchingStategy
    {
        public virtual bool TryMatch (FieldController field, TileController firstTile, TileController secondTile) 
        { return false; }
    }
}
