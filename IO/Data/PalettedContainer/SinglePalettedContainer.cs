using DivaMC.Protocol.IO.Serialization;

namespace DivaMC.Protocol.IO.Data.PalettedContainer
{
    internal sealed class SinglePalettedContainer : IPalettedContainer
    {
        public int Bits => 0;
        public int Length { get; }
        public int Value { get; set; }
        public int MaximumMappings => 1;
        public int CurrentMappings => 1;

        public SinglePalettedContainer(int length, int value)
        {
            Length = length;
            Value = value;
        }
        
        public bool CanMap(int value)
        {
            return Value == value;
        }
        public int this[int index]
        {
            get
            {
                if (index < 0 || index >= Length) throw new ArgumentOutOfRangeException();
                return Value;
            }
            set
            {
                if (index < 0 || index >= Length) throw new ArgumentOutOfRangeException();
                if (Length > 1) throw new InvalidOperationException();
                Value = value;
            }
        }
        
        public void Serialize(Stream stream)
        {
            stream.WriteVS32(Value);
        }
        public static IPalettedContainer Deserialize(Stream stream)
        {
            throw new NotImplementedException();
        }
    }
}