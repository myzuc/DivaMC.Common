using System.Numerics;
using DivaMC.Protocol.IO.Serialization;

namespace DivaMC.Protocol.IO.Data.PalettedContainer
{
    public sealed class PalettedContainer : IByteSerializable<PalettedContainer>
    {
        public int Length { get; }
        private IPalettedContainer Container { get; set; }
        
        public required int MinimumIndirectBits { get; init; }
        public required int MaximumIndirectBits { get; init; }
        public required int DirectBits { get; init; }

        public PalettedContainer(int length)
        {
            Length = length;
            Container = new SinglePalettedContainer(Length, 0);
        }
        
        public int this[int index]
        {
            get
            {
                if (index < 0 || index >= Length) throw new ArgumentOutOfRangeException();
                return Container[index];
            }
            set
            {
                if (index < 0 || index >= Length) throw new ArgumentOutOfRangeException();
                if (!Container.CanMap(value))
                {
                    IPalettedContainer newContainer;
                    if (Container.Bits < MaximumIndirectBits)
                    {
                        newContainer = new IndirectPalettedContainer(Length, int.Max(Container.Bits + 1, MinimumIndirectBits));
                    }
                    else
                    {
                        newContainer = new DirectPalettedContainer(Length, DirectBits);
                    }
                    for (int i = 0; i < Length; i++)
                    {
                        newContainer[i] = Container[i];
                    }
                    Container = newContainer;
                }
                Container[index] = value;
                int mappings = Container.CurrentMappings;
                int bitsNeeded = mappings <= 1 ? 0 : BitOperations.Log2((uint)mappings - 1) + 1;
                if (bitsNeeded < Container.Bits)
                {
                    IPalettedContainer newContainer = Container;
                    if (Container.Bits >= MinimumIndirectBits && bitsNeeded <= 0)
                    {
                        newContainer = new SinglePalettedContainer(Length, Container[0]);
                    }
                    if (Container.Bits > MaximumIndirectBits && bitsNeeded <= MaximumIndirectBits)
                    {
                        newContainer = new IndirectPalettedContainer(Length, bitsNeeded);
                        for (int i = 0; i < Length; i++)
                        {
                            newContainer[i] = Container[i];
                            Container = newContainer;
                        }
                    }
                    Container = newContainer;
                }
            }
        }
        
        public void Serialize(Stream stream)
        {
            stream.WriteVS32(Container.Bits);
            Container.Serialize(stream);
        }
        public static PalettedContainer Deserialize(Stream stream)
        {
            throw new NotImplementedException();
        }
    }
}