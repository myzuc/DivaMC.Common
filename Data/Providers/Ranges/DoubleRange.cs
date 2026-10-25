using DivaMC.Common.IO.Serialization;
using fNbt;

namespace DivaMC.Common.Data.Providers.Ranges
{
    public readonly record struct DoubleRange : INbtSerializable<DoubleRange>
    {
        public readonly double Minimum = 0.0;
        public readonly double Maximum = 0.0;
        public DoubleRange()
        {
            
        }
        public DoubleRange(double range)
        {
            Minimum = range;
            Maximum = range;
        }
        public DoubleRange(double minimum, double maximum)
        {
            Minimum = minimum;
            Maximum = maximum;
        }
        public NbtTag ToNbt()
        {
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (Minimum == Maximum) return NbtDouble.FromF64(Minimum);
            NbtCompound nbt = new();  
            NbtDouble.FromF64(Minimum).Rename("min").AddTo(nbt);
            NbtDouble.FromF64(Minimum).Rename("max").AddTo(nbt);
            return nbt;
        }
        public static DoubleRange FromNbt(NbtTag nbt)
        {
            if (nbt is NbtCompound nbtCompound)
            {
                return new(
                    nbtCompound["min"]?.DoubleValue ?? throw new InvalidDataException("Minimum not set"),
                    nbtCompound["max"]?.DoubleValue ?? throw new InvalidDataException("Maximum not set")
                    );
            }
            return new(nbt.DoubleValue);
        }
    }
}