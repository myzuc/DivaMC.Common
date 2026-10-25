using DivaMC.Common.Common;
using DivaMC.Common.Data;
using DivaMC.Common.IO.Serialization;
using DivaMC.Common.Registries;
using fNbt;

namespace DivaMC.Common.World.Dimensions
{
    public sealed record DimensionType : INbtSerializable<DimensionType>
    {
        public static readonly Identifier RegistryName = "minecraft:dimension_type";
        
        public static readonly DimensionType Stub = new()
        {
            Depth = 0,
            Height = 16,
            LogicalHeight = 16,
            CoordinateScale = 1.0,
                                                
            Clock = null,
            HasNoTime = true,
            Timelines = [],
                  
            Skybox = null,
            AmbientLight = 0.0f,
            CardinalLight = null,
            HasSkylight = false,
                                                
            InfiniteBurnBlockTag = new(),

            HasCeiling = false,
            HasDragon = false,
                                                
            MobSpawnLight = 0,
            MobSpawnBlocklightLimit = 0,
        };
        public static readonly Registry<DimensionType> StubRegistry = new(RegistryName)
        {
            Entries =
            {
                ["minecraft:overworld"] = Stub
            }
        };
        public static readonly RegistryTags StubRegistryTags = new(RegistryName);
        
        public required int Depth { get; set; }
        public required int Height { get; set; }
        public required int LogicalHeight { get; set; }
        public required double CoordinateScale { get; set; }
        
        public Identifier? Clock { get; set; } = null;
        public bool? HasNoTime { get; set; } = null;
        public required List<Identifier> Timelines { get; set; }
        
        public string? Skybox { get; set; } = null;
        //todo: attribute provider thingy
        public required float AmbientLight { get; set; }
        public string? CardinalLight { get; set; } = null;
        public required bool HasSkylight { get; set; }
        
        public required IdentifierSelector InfiniteBurnBlockTag { get; set; }
        
        public required bool HasCeiling { get; set; }
        public required bool HasDragon { get; set; }
        
        public required object MobSpawnLight { get; set; } //todo: implement integer providers
        public required int MobSpawnBlocklightLimit { get; set; }
        
        public DimensionType()
        {
            
        }
        public NbtTag ToNbt()
        {
            NbtCompound nbt = new();
            NbtInt.FromS32(Depth).Rename("min_y").AddTo(nbt);
            NbtInt.FromS32(Height).Rename("height").AddTo(nbt);
            NbtInt.FromS32(LogicalHeight).Rename("logical_height").AddTo(nbt);
            NbtDouble.FromF64(CoordinateScale).Rename("coordinate_scale").AddTo(nbt);
            
            if (Clock.HasValue) NbtString.FromT0A(Clock).Rename("default_clock").AddTo(nbt);
            if (HasNoTime.HasValue) NbtByte.FromU1(HasNoTime.Value).Rename("has_fixed_time").AddTo(nbt);
            if (Timelines.Count == 1) NbtString.FromT0A(Timelines.First()).Rename("timelines").AddTo(nbt);
            else NbtList.FromIEnumerable(Timelines).Rename("timelines").AddTo(nbt);
            
            if (Skybox is not null) NbtString.FromT0A(Skybox).Rename("skybox").AddTo(nbt);
            new NbtCompound().Rename("attributes").AddTo(nbt); //todo: attribues
            NbtFloat.FromF32(AmbientLight).Rename("ambient_light").AddTo(nbt);
            if (CardinalLight is not null) NbtString.FromT0A(CardinalLight).Rename("cardinal_light").AddTo(nbt);
            NbtByte.FromU1(HasSkylight).Rename("has_skylight").AddTo(nbt);
            
            InfiniteBurnBlockTag.ToNbt().Rename("infiniburn").AddTo(nbt);
            
            NbtByte.FromU1(HasCeiling).Rename("has_ceiling").AddTo(nbt);
            NbtByte.FromU1(HasDragon).Rename("has_ender_dragon_fight").AddTo(nbt);
            
            NbtInt.FromS32((int)MobSpawnLight).Rename("monster_spawn_light_level").AddTo(nbt); //todo: int providers
            NbtInt.FromS32(MobSpawnBlocklightLimit).Rename("monster_spawn_block_light_limit").AddTo(nbt);
            return nbt;
        }
        public static DimensionType FromNbt(NbtTag nbt)
        {
            throw new NotImplementedException();
        }
    }
}