namespace DivaMC.Common.IO.Data.PalettedContainer
{
    internal interface IPalettedContainer
    {
        public int Length { get; }
        public int Bits { get; }
        public int MaximumMappings { get; }
        public int CurrentMappings { get; }
        public bool CanMap(int value);
        public int this[int index] { get; set; }

        public void Serialize(Stream stream);
        public static abstract IPalettedContainer Deserialize(Stream stream);
    }
}