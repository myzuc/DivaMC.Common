using DivaMC.Protocol.Common;
using DivaMC.Protocol.IO.Serialization;
using DivaMC.Protocol.Registries;
using fNbt;

namespace DivaMC.Protocol.Data
{
    using BlockStateProvider = Providers.BlockStateProviders.BlockStateProvider;
    using CopyProperties = Providers.BlockStateProviders.CopyProperties;

    public sealed record BlockTransformer : INbtSerializable<BlockTransformer>
    {
        public static readonly Identifier RegistryName = "minecraft:block_transformer";

        public static readonly BlockTransformer Stub = new()
        {
            BlockStateProvider = new CopyProperties
            {
                Source = Identifier.EmptyBlock
            }
        };
        public static readonly Registry<NbtList<BlockTransformer>> StubRegistry = new(RegistryName)
        {
            Entries = {
                ["minecraft:hoe"] = [Stub],
                ["minecraft:shovel"] = [Stub],
                ["minecraft:axe"] = [Stub],
            }
        };
        public static readonly RegistryTags StubRegistryTags = new(RegistryName);
        
        public required BlockStateProvider BlockStateProvider { get; set; }
        public SoundEvent? Sound { get; set; }
        public string? Particle { get; set; }
        public List<string>? DisallowedFaces { get; set; }
        public Identifier? Loot  { get; set; }
        public string? DropPosition { get; set; }
        public bool? TriggerUpdate { get; set; }
        public string? Type { get; set; }
        public bool? ConsumeItem { get; set; }
        public int? DamageItem { get; set; }
        public BlockTransformer()
        {
            
        }
        public NbtTag ToNbt()
        {
            NbtCompound nbt = new();
            BlockStateProvider.ToNbt().Rename("block_state_provider").AddTo(nbt);
            Sound?.ToNbt().Rename("sound").AddTo(nbt);
            if (Particle is not null) NbtString.FromT0A(Particle).Rename("particle").AddTo(nbt);
            if (DisallowedFaces is not null) NbtList.FromIEnumerable(DisallowedFaces.Select(NbtString.FromT0A)).Rename("disallowed_faces").AddTo(nbt);
            if (Loot.HasValue) NbtString.FromT0A(Loot).Rename("loot").AddTo(nbt);
            if (DropPosition is not null) NbtString.FromT0A(DropPosition).Rename("drop_strategy").AddTo(nbt);
            if (TriggerUpdate.HasValue) NbtByte.FromU1(TriggerUpdate.Value).Rename("update_from_neighbors").AddTo(nbt);
            if (Type is not null) NbtString.FromT0A(Type).Rename("transform_type").AddTo(nbt);
            if (ConsumeItem.HasValue) NbtByte.FromU1(ConsumeItem.Value).Rename("consume_on_use").AddTo(nbt);
            if (DamageItem.HasValue) NbtInt.FromS32(DamageItem.Value).Rename("item_damage_per_use").AddTo(nbt);
            return nbt;
        }
        public static BlockTransformer FromNbt(NbtTag nbt)
        {
            throw new NotImplementedException();
        }
    }
}