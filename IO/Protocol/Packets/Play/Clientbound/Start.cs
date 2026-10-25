using System.Collections.Immutable;
using DivaMC.Common.Game.Objects;
using DivaMC.Common.IO.Serialization;
using DivaMC.Common.Primitives;

namespace DivaMC.Common.IO.Protocol.Packets.Play.Clientbound
{
    public sealed record Start : Packet
    {
        static Start()
        {
            PacketSignature signature = new PacketSignature(
                ProtocolDirection.Clientbound,
                ProtocolStage.Play,
                0x50
            );
            Packet.Register<Start>(signature, Deserialize);
        }
        
        public required int EntityId { get; init; }
        public required bool Hardcore { get; init; }
        public required IImmutableList<Identifier> DimensionNames { get; set; }
        public required int TablistSize { get; init; }
        public required int ViewDistance { get; init; }
        public required int SimulationDistance { get; init; }
        public required bool ReduceDebugInfo { get; init; }
        public required bool ShowRespawnScreen { get; init; }
        public required bool LimitedCrafting { get; init; }
        public required RespawnInfo RespawnInfo { get; init; }
        public required bool Authenticated { get; init; }
        public required bool HasCensorship { get; init; }
        
        protected override void Serialize(Stream stream, object registry)
        {
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
        private static Packet Deserialize(Stream stream, object registry)
        {
            throw new NotImplementedException();
        }
    }
}