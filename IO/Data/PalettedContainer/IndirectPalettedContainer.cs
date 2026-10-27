using DivaMC.Common.IO.Serialization;

namespace DivaMC.Common.IO.Data.PalettedContainer
{
    internal class IndirectPalettedContainer : IPalettedContainer
    {
        public int Length { get; }
        public int Bits { get; }
        public int MaximumMappings => 1 << Bits;
        public int CurrentMappings => MappingCounts.Count(mapping => mapping > 0);
        
        private int[] Mappings { get; }
        private int[] MappingCounts { get; }
        private PackedArray Data { get; }

        public IndirectPalettedContainer(int length, int bits)
        {
            if (bits < 0 || bits > 31) throw new ArgumentOutOfRangeException();
            Length = length;
            Bits = bits;
            Mappings = new int[MaximumMappings];
            MappingCounts = new int[MaximumMappings];
            MappingCounts[0] = Length;
            Data = new(Bits, Length);
        }
        
        public bool CanMap(int value)
        {
            if (MappingCounts.Any(mapping => mapping <= 0)) return true;
            return Mappings.Contains(value);
        }
        public int this[int index]
        {
            get
            {
                if (index < 0 || index >= Length) throw new ArgumentOutOfRangeException();
                return Mappings[Data[index]];
            }
            set
            {
                int previousMapping = (int)Data[index];
                MappingCounts[previousMapping]--;
                if (index < 0 || index >= Length) throw new ArgumentOutOfRangeException();
                int mapping = Mappings.IndexOf(value);
                if (mapping < 0)
                {
                    if (!CanMap(value)) throw new InvalidOperationException();
                    for (int i = 0; i < Mappings.Length; i++)
                    {
                        if (MappingCounts[i] > 0) continue;
                        Mappings[i] = value;
                        mapping = i;
                        break;
                    }
                }
                MappingCounts[mapping]++;
                Data[index] = mapping;
            }
        }
        
        public void Serialize(Stream stream)
        {
            stream.WriteVS32(Mappings.Length);
            for (int i = 0; i < Mappings.Length; i++)
            {
                stream.WriteVS32(Mappings[i]);
            }
            stream.Write(Data, (Data.Bits, Data.Length));
        }
        public static IPalettedContainer Deserialize(Stream stream)
        {
            throw new NotImplementedException();
        }
    }
}