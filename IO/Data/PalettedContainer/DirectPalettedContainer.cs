using DivaMC.Common.IO.Serialization;

namespace DivaMC.Common.IO.Data.PalettedContainer
{
    internal class DirectPalettedContainer : IPalettedContainer
    {
        public int Length { get; }
        public int Bits { get; }
        public int MaximumMappings => (1 << Bits) - 1;
        public int CurrentMappings => Data.Distinct().Count();
        
        private PackedArray Data { get; }

        public DirectPalettedContainer(int length, int bits)
        {
            if (bits < 0 || bits > 31) throw new ArgumentOutOfRangeException();
            Length = length;
            Bits = bits;
            Data = new(Bits, Length);
        }
        
        public bool CanMap(int value)
        {
            return true;
        }
        public int this[int index]
        {
            get
            {
                if (index < 0 || index >= Length) throw new ArgumentOutOfRangeException();
                return (int)Data[index];
            }
            set
            {
                if (index < 0 || index >= Length) throw new ArgumentOutOfRangeException();
                Data[index] = value;
            }
        }
        
        public void Serialize(Stream stream)
        {
            stream.Write(Data, (Data.Bits, Data.Length));
        }
        public static IPalettedContainer Deserialize(Stream stream)
        {
            throw new NotImplementedException();
        }
    }
}