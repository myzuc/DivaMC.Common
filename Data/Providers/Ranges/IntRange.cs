using DivaMC.Common.IO.Serialization;
using fNbt;

namespace DivaMC.Common.Data.Providers.Ranges
{
    public readonly record struct IntRange : INbtSerializable<IntRange>
    {
        //todo: is this used? does this work?
        public readonly int Minimum = 0;
        public readonly int Maximum = 0;
        public IntRange()
        {
            
        }
        public IntRange(int range)
        {
            Minimum = range;
            Maximum = range;
        }
        public IntRange(int minimum, int maximum)
        {
            Minimum = minimum;
            Maximum = maximum;
        }
        public NbtTag ToNbt()
        {
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (Minimum == Maximum) return NbtInt.FromS32(Minimum);
            NbtCompound nbt = new();  
            NbtInt.FromS32(Minimum).Rename("min").AddTo(nbt);
            NbtInt.FromS32(Minimum).Rename("max").AddTo(nbt);
            return nbt;
        }
        public static IntRange FromNbt(NbtTag nbt)
        {
            if (nbt is NbtCompound nbtCompound)
            {
                return new(
                    nbtCompound["min"]?.IntValue ?? throw new InvalidDataException("Minimum not set"),
                    nbtCompound["max"]?.IntValue ?? throw new InvalidDataException("Maximum not set")
                    );
            }
            return new(nbt.IntValue);
        }
    }
}