namespace DivaMC.Common.World.Common
{
    public readonly record struct SectionLocalLocation
    {
        public static readonly int MinX = 0;
        public static readonly int MaxX = 15;
        public static readonly int MinY = 0;
        public static readonly int MaxY = 15;
        public static readonly int MinZ = 0;
        public static readonly int MaxZ = 15;
        
        public int X => Index & 15;
        public int Y => (Index >> 8) & 15;
        public int Z => (Index >> 4) & 15;
        public ushort Index { get; } = 0;
        public SectionLocalLocation()
        {
            
        }
        public SectionLocalLocation(ushort index)
        {
            if (index >= 4096) throw new ArgumentOutOfRangeException();
            Index = index;
        }
        public SectionLocalLocation(int x, int y, int z)
        {
            if (x < MinX || x > MaxX) throw new ArgumentOutOfRangeException();
            if (y < MinY || y > MaxY) throw new ArgumentOutOfRangeException();
            if (z < MinZ || z > MaxZ) throw new ArgumentOutOfRangeException();
            Index = (ushort)(x | (z << 4) | (y << 8));
        }
    }
}