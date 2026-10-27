namespace DivaMC.Common.World.Tiles
{
    public sealed record StoneTile : Tile
    {
        static StoneTile()
        {
            Register(new StoneTile(), 1);
        }

        public override bool IsAir => false;
        public override bool HasFluid => false;
    }
}