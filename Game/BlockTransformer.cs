using System.Collections.Immutable;
using DivaMC.Common.Game.Objects;
using BlockStateProviders = DivaMC.Common.Game.Providers.BlockStateProviders;
using DivaMC.Common.Game.Registries;
using DivaMC.Common.IO.Serialization;
using DivaMC.Common.Primitives;
using fNbt;

namespace DivaMC.Common.Game
{
    public sealed record BlockTransformer : INbtSerializable<BlockTransformer>
    {
        public static readonly Identifier RegistryName = "minecraft:block_transformer";

        public static readonly BlockTransformer Stub = new()
        {
            BlockStateProvider = new BlockStateProviders.CopyProperties
            {
                Source = Identifier.EmptyBlock
            }
        };
        public static readonly Registry<BlockTransformer> StubRegistry = new()
        {
            Id = RegistryName,
            Entries = [
                new("minecraft:shovel", Stub),
            ]
        };
        
        public required BlockStateProviders.BlockStateProvider BlockStateProvider { get; init; }
        public SoundEvent? Sound { get; init; }
        public string? Particle { get; init; }
        public IImmutableList<string>? DisallowedFaces { get; init; }
        public Identifier? Loot  { get; init; }
        public string? DropPosition { get; init; }
        public bool? TriggerUpdate { get; init; }
        public string? Type { get; init; }
        public bool? ConsumeItem { get; init; }
        public int? DamageItem { get; init; }
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