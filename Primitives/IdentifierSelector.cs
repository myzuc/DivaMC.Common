using System.Collections.Immutable;
using DivaMC.Common.IO.Serialization;
using fNbt;

namespace DivaMC.Common.Primitives
{
    public sealed record IdentifierSelector : INbtSerializable<IdentifierSelector>
    {
        public IImmutableList<Identifier> Identifiers { get; init; } = [];
        public IImmutableList<Identifier> Tags { get; init; } = [];
        public IdentifierSelector()
        {
            
        }
        public IdentifierSelector(string key)
        {
            if (IsTag(key)) Tags = [ToNonTag(key)];
            else Identifiers = [key];
        }
        public IdentifierSelector(Identifier key)
        {
            Identifiers = [key];
        }
        public IdentifierSelector(IImmutableList<string> keys)
        {
            Tags = keys.Where(IsTag).Select(ToNonTag).ToImmutableArray();
            Identifiers = keys.Where(key => !IsTag(key)).Select(ToNonTag).ToImmutableArray();
        }
        
        public static implicit operator IdentifierSelector(string value)
        {
            return new(value);
        }
        public static implicit operator IdentifierSelector(Identifier value)
        {
            return new(value);
        }

        private static bool IsTag(string key)
        {
            return key.StartsWith('#');
        }
        private static Identifier ToNonTag(string key)
        {
            if (key.StartsWith('#')) return key[1..];
            return key;
        }
        private static string ToTag(Identifier key)
        {
            if (key.FullKey.StartsWith('#')) return key;
            return $"#{key}";
        }
        
        public NbtTag ToNbt()
        {
            if (Identifiers.Count == 1 && Tags.Count == 0) return NbtString.FromT0A(Identifiers[0]);
            if (Identifiers.Count == 0 && Tags.Count == 1) return NbtString.FromT0A($"#{Tags[0]}");
            return new NbtList(Tags.Select(ToTag).Concat(Identifiers.Select(key => key.FullKey)).Select(NbtString.FromT0A));
        }
        public static IdentifierSelector FromNbt(NbtTag nbt)
        {
            throw new NotImplementedException();
        }
    }
}