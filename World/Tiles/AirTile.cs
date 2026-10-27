namespace DivaMC.Protocol.World.Tiles
{
    public sealed record AirTile : Tile
    {
        static AirTile()
        {
            Register(new AirTile(), 0);
        }
        
        public override bool IsAir => true;
        public override bool HasFluid => false;
    }
}