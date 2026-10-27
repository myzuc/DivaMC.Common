using DivaMC.Common.Common;
using DivaMC.Common.IO.Serialization;
using DivaMC.Common.World.Common.Objects;

namespace DivaMC.Common.IO.Protocol.Packets.Game
{
    public sealed class CBSpawnPacket : Packet
    {
        static CBSpawnPacket()
        {
            Register<CBSpawnPacket>(new(ProtocolDirection.Clientbound, ProtocolStage.Game,0x32), Deserialize);
        }
        
        public required int EntityId { get; set; }
        public required bool Hardcore { get; set; }
        public required List<Identifier> DimensionNames { get; set; }
        public required int TablistSize { get; set; }
        public required int ViewDistance { get; set; }
        public required int SimulationDistance { get; set; }
        public required bool ReduceDebugInfo { get; set; }
        public required bool ShowRespawnScreen { get; set; }
        public required bool LimitedCrafting { get; set; }
        public required RespawnInfo RespawnInfo { get; set; }
        public required bool Authenticated { get; set; }
        public required bool HasCensorship { get; set; }
        
        public override void Serialize(Stream stream, PacketContext context)
        {
            base.Serialize(stream, context);
            stream.WriteS32(EntityId);
            stream.WriteU1(Hardcore);
            stream.WriteVS32(DimensionNames.Count);
            foreach (Identifier dimensionName in DimensionNames)
            {
                stream.Write(dimensionName);
            }
            stream.WriteVS32(TablistSize);
            stream.WriteVS32(ViewDistance);
            stream.WriteVS32(SimulationDistance);
            stream.WriteU1(ReduceDebugInfo);
            stream.WriteU1(ShowRespawnScreen);
            stream.WriteU1(LimitedCrafting);
            stream.Write(RespawnInfo);
            stream.WriteU1(Authenticated);
            stream.WriteU1(HasCensorship);
        }
        new private static Packet Deserialize(Stream stream, PacketContext context)
        {
            throw new NotImplementedException();
        }
    }
}