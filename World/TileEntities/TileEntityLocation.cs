using DivaMC.Protocol.IO.Serialization;

namespace DivaMC.Protocol.World.TileEntities
{
    public readonly record struct TileEntityLocation : IBinarySerializable<TileEntityLocation>
    {
        public static readonly int MinX = 0;
        public static readonly int MaxX = 15;
        public static readonly int MinY = short.MinValue;
        public static readonly int MaxY = short.MaxValue;
        public static readonly int MinZ = 0;
        public static readonly int MaxZ = 15;
        
        public int X => XZ >> 4;
        public short Y { get; } = 0;
        public int Z => XZ & 0x0F;
        private readonly byte XZ = 0x00;
        public TileEntityLocation()
        {
            
        }
        private TileEntityLocation(byte xz, short y)
        {
            XZ = xz;
            Y = y;
        }
        public TileEntityLocation(int x, int y, int z)
        {
            if (x < MinX || x > MaxX) throw new ArgumentOutOfRangeException();
            if (y < MinY || y > MaxY) throw new ArgumentOutOfRangeException();
            if (z < MinZ || z > MaxZ) throw new ArgumentOutOfRangeException();
            XZ = (byte)((x << 4) | y);
            Y = (short)y;
        }
        public void Serialize(Stream stream)
        {
            stream.WriteU8(XZ);
            stream.WriteS16(Y);
        }
        public static TileEntityLocation Deserialize(Stream stream)
        {
            byte xz = stream.ReadU8();
            short y = stream.ReadS16();
            return new(xz, y);
        }
    }
}