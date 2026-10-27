using DivaMC.Protocol.IO.Serialization;
using DivaMC.Protocol.World.Common;

namespace DivaMC.Protocol.World.Chunks.Light
{
    public sealed class LightSection : IBinarySerializable<LightSection>
    {
        public bool IsEmpty => Data.All(b => b == 0);
        private byte[] Data { get; } = new byte[2048];

        public LightSection()
        {
            
        }
        
        public int this[SectionLocalLocation xyz]
        {
            get
            {
                int index = xyz.Index >> 1;
                int shift = (xyz.Index % 2) * 4;
                int mask = 15 << shift;
                return ((Data[index] & mask) >> shift);
            }
            set
            {
                if (value < 0 || value >= 16) throw new ArgumentOutOfRangeException();
                int index = xyz.Index >> 1;
                int shift = (xyz.Index % 2) * 4;
                int mask = 15 << shift;
                Data[index] &= (byte)~mask;
                Data[index] |= (byte)(value << shift);
            }
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