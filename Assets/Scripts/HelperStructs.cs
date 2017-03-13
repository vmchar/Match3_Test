
namespace Match3Test
{
    public enum ElementType
    {
        Croissant,
        SugarCookie,
        Donut,
        Danish,
        Cupcake,
        Macaroon,
        SmallBomb
    }

    public delegate void ElementDestroyed (ElementController element);
}