using System.Drawing;
using DivaMC.Common.Common;
using DivaMC.Common.IO.Serialization;
using DivaMC.Common.Registries;
using fNbt;

namespace DivaMC.Common.World.Biomes
{
    public sealed record Biome : INbtSerializable<Biome>
    {
        public static readonly Identifier RegistryName = "minecraft:worldgen/biome";
        
        public static readonly Biome Stub = new()
        {
            Ambient = new()
            {
                WaterColor = Color.Empty,
                FoliageColor = null,
                DryFoliageColor = null,
                GrassColor = null,
                GrassColorModifier = null,
            },
            Precipitation = false,
            Humidity = 0,
            Temperature = 0,
            TemperatureModifier = null,
        };
        public static readonly Registry<Biome> StubRegistry = new(RegistryName)
        {
            Entries =
            {
                ["minecraft:plains"] = Stub
            }
        };
        public static readonly RegistryTags StubRegistryTags = new(RegistryName);
        
        public required Ambient Ambient { get; set; }
        
        public required bool Precipitation { get; set; }
        public required float Humidity { get; set; }
        public required float Temperature { get; set; }
        public string? TemperatureModifier { get; set; } = null;
        //todo: environment attributes
        //todo: carvers
        //todo: features
        
        public Biome()
        {
            
        }
        public NbtTag ToNbt()
        {
            NbtCompound nbt = new();
            Ambient.ToNbt().Rename("effects").AddTo(nbt);
            NbtByte.FromU1(Precipitation).Rename("has_precipitation").AddTo(nbt);
            NbtFloat.FromF32(Humidity).Rename("downfall").AddTo(nbt);
            NbtFloat.FromF32(Temperature).Rename("temperature").AddTo(nbt);
            if (TemperatureModifier is not null) NbtString.FromT0A(TemperatureModifier).Rename("temperature_modifier").AddTo(nbt);
            return nbt;
        }
        public static Biome FromNbt(NbtTag nbt)
        {
            throw new NotImplementedException();
        }
    }
}