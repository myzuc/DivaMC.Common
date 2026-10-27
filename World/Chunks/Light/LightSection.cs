using DivaMC.Common.IO.Serialization;
using DivaMC.Common.World.Common;

namespace DivaMC.Common.World.Chunks.Light
{
    public sealed class LightSection : IBinarySerializable<LightSection>
    {
        public bool IsEmpty => Data.All(b => b == 0);
        private byte[] Data { get; } = new byte[2048];

        public LightSection()
        {
            
        }
        
        public int this[int x, int y, int z]
        {
            get
            {
                return this[new(x, y, z)];
            }
            set
            {
                this[new(x, y, z)] = value;
            }
        }
        public int this[SectionLocalLocation location]
        {
            get
            {
                int index = location.Index >> 1;
                int shift = (location.Index % 2) * 4;
                int mask = 15 << shift;
                return ((Data[index] & mask) >> shift);
            }
            set
            {
                if (value < 0 || value >= 16) throw new ArgumentOutOfRangeException();
                int index = location.Index >> 1;
                int shift = (location.Index % 2) * 4;
                int mask = 15 << shift;
                Data[index] &= (byte)~mask;
                Data[index] |= (byte)(value << shift);
            }
        }
        
        public void Serialize(Stream stream)
        {
            stream.WriteVS32(Data.Length);
            stream.WriteU8A(Data);
        }
        public static LightSection Deserialize(Stream stream)
        {
            throw new NotImplementedException();
        }
    }
}