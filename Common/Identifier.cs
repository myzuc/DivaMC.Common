using System.Text.Json.Serialization;
using DivaMC.Common.IO.Serialization;
using fNbt;

namespace DivaMC.Common.Common
{
    public readonly record struct Identifier : IBinarySerializable<Identifier>, INbtSerializable<Identifier>
    {
        public static readonly Identifier Empty = "mineraft:empty";
        public static readonly Identifier EmptySound = "minecraft:intentionally_empty";
        public static readonly Identifier EmptyTexture = "minecraft:missingno";
        public static readonly Identifier EmptyBlock = "minecraft:air";
        
        private const string ValidNamespaceCharacters = "abcdefghijklmnopqrstuvwxyz0123456789.-_";
        private const string ValidValueCharacters = "abcdefghijklmnopqrstuvwxyz0123456789.-_/";
        
        [JsonInclude]
        public string FullKey
        {
            get
            {
                return $"{Namespace}:{Path}";
            }
        }
        [JsonIgnore]
        public string Namespace { get; }
        [JsonIgnore]
        public string Path { get; }
        public Identifier()
        {
            Namespace = "minecraft";
            Path = "empty";
        }
        public Identifier(string @namespace, string path)
        {
            Namespace = @namespace;
            Path = path;
            if (Namespace.Any(c => !ValidNamespaceCharacters.Contains(c))) throw new ArgumentException($"Invalid identifier namespace '{Namespace}'!");
            if (Path.Any(c => !ValidValueCharacters.Contains(c))) throw new ArgumentException($"Invalid identifier value '{Path}'!");
        }
        public Identifier(string fullIdentifier)
        {
            string[] parts = fullIdentifier.Split(':');
            if (parts.Length > 2) throw new ArgumentException($"Invalid identifier '{fullIdentifier}'!");
            Namespace = parts.Length > 1 ? parts[0] : "minecraft";
            Path = parts[^1];
            if (Namespace.Any(c => !ValidNamespaceCharacters.Contains(c))) throw new ArgumentException($"Invalid identifier namespace '{Namespace}'!");
            if (Path.Any(c => !ValidValueCharacters.Contains(c))) throw new ArgumentException($"Invalid identifier value '{Path}'!");
        }
        
        public override string ToString()
        {
            return FullKey;
        }
        
        public static implicit operator Identifier(string value)
        {
            return new(value);
        }
        public static implicit operator string(Identifier value)
        {
            return value.FullKey;
        }
        
        static Identifier IBinarySerializable<Identifier>.Deserialize(Stream stream)
        {
            return new(stream.ReadT0AVS32());
        }
        void IBinarySerializable<Identifier>.Serialize(Stream stream)
        {
            stream.WriteT0AVS32(FullKey);
        }
        public NbtTag ToNbt()
        {
            return new NbtString(FullKey);
        }
        public static Identifier FromNbt(NbtTag nbt)
        {
            return new(nbt.StringValue);
        }
    }
}