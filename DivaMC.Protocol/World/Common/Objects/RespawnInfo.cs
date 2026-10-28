using DivaMC.Protocol.Common;
using DivaMC.Protocol.Common.Enums;
using DivaMC.Protocol.IO.Serialization;

namespace DivaMC.Protocol.World.Common.Objects
{
    public sealed record RespawnInfo : IByteSerializable<RespawnInfo>
    {
        public required int CurrentDimensionTypeId { get; init; }
        public required Identifier CurrentDimensionName { get; init; }
        public required long NoiseSeed { get; init; }
        public required Gamemode CurrentGamemode { get; init; }
        public required Gamemode PreviousGamemode { get; init; }
        public required bool IsDebugWorld { get; init; }
        public required bool IsSuperflatWorld { get; init; }
        public required GlobalLocation? DeathLocation { get; init; }
        public required int PortalCooldown { get; init; }
        public required int OceanHeight { get; init; }
        
        public RespawnInfo()
        {
            
        }
        
        public void Serialize(Stream stream)
        {
            stream.WriteVS32(CurrentDimensionTypeId);
            stream.Write(CurrentDimensionName);
            stream.WriteS64(NoiseSeed);
            stream.WriteS8((sbyte)CurrentGamemode);
            stream.WriteS8((sbyte)PreviousGamemode);
            stream.WriteU1(IsDebugWorld);
            stream.WriteU1(IsSuperflatWorld);
            stream.WriteU1(DeathLocation is not null);
            if (DeathLocation is not null)
            {
                stream.Write(DeathLocation);
            }
            stream.WriteVS32(PortalCooldown);
            stream.WriteVS32(OceanHeight);
        }
        public static RespawnInfo Deserialize(Stream stream)
        {
            return new()
            {
                CurrentDimensionTypeId = stream.ReadVS32(),
                CurrentDimensionName = stream.Read<Identifier>(),
                NoiseSeed = stream.ReadS64(),
                CurrentGamemode = (Gamemode)stream.ReadS8(),
                PreviousGamemode = (Gamemode)stream.ReadS8(),
                IsDebugWorld = stream.ReadU1(),
                IsSuperflatWorld = stream.ReadU1(),
                DeathLocation = stream.ReadU1() ? stream.Read<GlobalLocation>() : null,
                PortalCooldown = stream.ReadVS32(),
                OceanHeight = stream.ReadVS32(),
            };
        }
    }
}