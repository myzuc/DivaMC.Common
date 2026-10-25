using DivaMC.Common.IO.Serialization;
using DivaMC.Common.Primitives;
using fNbt;

namespace DivaMC.Common.Game.Registries
{
    public readonly record struct RegistryEntry : IBinarySerializable<RegistryEntry>
    {
        public readonly Identifier Id;
        public readonly NbtTag? Value;
        public RegistryEntry()
        {
            Id = new();
            Value = null;
        }
        public RegistryEntry(Identifier id, NbtTag? value)
        {
            Id = id;
            Value = value;
        }
        
        public void Serialize(Stream stream)
        {
            stream.Write(Id);
            stream.WriteU1(Value is not null);
            if (Value is not null) stream.WriteNbt(Value);
        }
        public static RegistryEntry Deserialize(Stream stream)
        {
            Identifier id = stream.Read<Identifier>();
            NbtTag? value = stream.ReadU1() ? stream.ReadNbt() : null;
            return new(id, value);
        }
    }
    public readonly record struct RegistryEntry<T> : IBinarySerializable<RegistryEntry<T>> where T : class, INbtSerializable<T>
    {
        public readonly Identifier Id;
        public readonly T? Value;
        public RegistryEntry()
        {
            Id = new();
            Value = null;
        }
        public RegistryEntry(Identifier id, T? value)
        {
            Id = id;
            Value = value;
        }
        
        public void Serialize(Stream stream)
        {
            stream.Write(Id);
            stream.WriteU1(Value is not null);
            if (Value is not null) stream.WriteNbt(Value);
        }
        public static RegistryEntry<T> Deserialize(Stream stream)
        {
            Identifier id = stream.Read<Identifier>();
            T? value = stream.ReadU1() ? stream.ReadNbt<T>() : null;
            return new(id, value);
        }

        public static explicit operator RegistryEntry(RegistryEntry<T> data)
        {
            return new(data.Id, data.Value?.ToNbt());
        }
        public static explicit operator RegistryEntry<T>(RegistryEntry data)
        {
            return new(data.Id, data.Value is not null ? T.FromNbt(data.Value) : null);
        }
    }
}