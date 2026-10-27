using DivaMC.Protocol.IO.Serialization;

namespace DivaMC.Protocol.IO.Protocol.Packets.Common
{
    public sealed class SBPingResponsePacket : Packet
    {
        static SBPingResponsePacket()
        {
            Register<SBPingResponsePacket>(new(ProtocolDirection.Serverbound, ProtocolStage.Configuration,0x05), Deserialize);
            Register<CBPingRequestPacket>(new(ProtocolDirection.Serverbound, ProtocolStage.Game,0x1C), Deserialize);
        }
        
        public required int Id { get; set; }
        
        public override void Serialize(Stream stream, PacketContext context)
        {
            base.Serialize(stream, context);
            throw new NotImplementedException();
        }
        new private static Packet Deserialize(Stream stream, PacketContext context)
        {
            return new SBPingResponsePacket()
            {
                Id = stream.ReadS32(),
            };
        }
    }
}