using DivaMC.Protocol.IO.Serialization;

namespace DivaMC.Protocol.World.Common
{
    public readonly record struct Location : IBinarySerializable<Location>
    {
        public static readonly int MinX = -33554432;
        public static readonly int MaxX = 33554431;
        public static readonly int MinY = -2048;
        public static readonly int MaxY = 2047;
        public static readonly int MinZ = -33554432;
        public static readonly int MaxZ = 33554431;
        
        private readonly long Value;
        public int X
        {
            get
            {
                int value = (int)((ulong)Value >> 38);
                if ((value & 0x02000000) != 0) value -= 0x04000000;
                return value;
            }
        }
        public int Z
        {
            get
            {
                int value = (int)(((ulong)Value >> 12) & 0x3FFFFFF);
                if ((value & 0x02000000) != 0) value -= 0x4000000;
                return value;
            }
        }
        public int Y
        {
            get
            {
                int value = (int)(((ulong)Value & 0xFFF));
                if ((value & 0x00000800) != 0) value -= 0x1000;
                return value;
            }
        }
        
        public Location()
        {
            Value = 0;
        }
        internal Location(long value)
        {
            Value = value;
        }
        public Location(int x, int y, int z)
        {
            if (x < MinX || x > MaxX) throw new ArgumentOutOfRangeException();
            if (y < MinY || y > MaxY) throw new ArgumentOutOfRangeException();
            if (z < MinZ || z > MaxZ) throw new ArgumentOutOfRangeException();
            Value = (long)((ulong)y | (ulong)(z << 12) | ((ulong)x << 38));
        }
        
        public static Location operator +(Location a, Location b)
        {
            return new(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }
        public static Location operator -(Location a, Location b)
        {
            return new(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }
        
        public void Serialize(Stream stream)
        {
            stream.WriteS64(Value);
        }
        public static Location Deserialize(Stream stream)
        {
            return new(stream.ReadS64());
        }
    }
}