namespace Tiles
{
    public struct TileProperties
    {
        public int Water { get; }
        public int Fertility { get; }

        public TileProperties(int water, int fertility)
        {
            Water = water;
            Fertility = fertility;
        }
    }
}