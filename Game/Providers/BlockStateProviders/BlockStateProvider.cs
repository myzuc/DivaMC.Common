using DivaMC.Common.IO.Serialization;
using fNbt;

namespace DivaMC.Common.Game.Providers.BlockStateProviders
{
    public abstract record BlockStateProvider : INbtSerializable<BlockStateProvider>
    {
        private readonly string Type;
        protected internal BlockStateProvider(string type)
        {
            Type = type;
            //todo: implement more
        }
        public virtual NbtTag ToNbt()
        {
            NbtCompound nbt = new();
            NbtString.FromT0A(Type).Rename("type").AddTo(nbt);
            return nbt;
        }
        public static BlockStateProvider FromNbt(NbtTag nbt)
        {
            throw new NotImplementedException();
        }
    }
}