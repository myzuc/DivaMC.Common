using DivaMC.Protocol.IO.Serialization;
using fNbt;

namespace DivaMC.Protocol.Data.Providers.BlockStateProviders
{
    public sealed record CopyProperties : BlockStateProvider
    {
        public static readonly string Type = "copy_properties";
        
        public required NbtBlockState Source { get; set; }
        public CopyProperties() : base(Type)
        {
            
        }
        
        public override NbtTag ToNbt()
        {
            NbtCompound nbt = (NbtCompound)base.ToNbt();
            Source.ToNbt().Rename("source").AddTo(nbt);
            return nbt;
        }
    }
}