using DivaMC.Common.IO.Serialization;
using fNbt;

namespace DivaMC.Common.Primitives
{
    public sealed record IdentifierSelector : INbtSerializable<IdentifierSelector>
    {
        public List<Identifier> Identifiers { get; set; } = [];
        public List<Identifier> Tags { get; set; } = [];
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
        public IdentifierSelector(List<string> keys)
        {
            Tags = keys.Where(IsTag).Select(ToNonTag).ToList();
            Identifiers = keys.Where(key => !IsTag(key)).Select(ToNonTag).ToList();
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
            if (Identifiers.Count == 1 && Tags.Count == 0) return NbtString.FromT0A(Identifiers.First());
            if (Identifiers.Count == 0 && Tags.Count == 1) return NbtString.FromT0A($"#{Tags.First()}");
            return NbtList.FromIEnumerable(Tags.Select(ToTag).Concat(Identifiers.Select(key => key.FullKey)).Select(NbtString.FromT0A));
        }
        public static IdentifierSelector FromNbt(NbtTag nbt)
        {
            throw new NotImplementedException();
        }
    }
}