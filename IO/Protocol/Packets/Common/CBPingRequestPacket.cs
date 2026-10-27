using DivaMC.Common.IO.Serialization;

namespace DivaMC.Common.IO.Protocol.Packets.Common
{
    public sealed class CBPingRequestPacket : Packet
    {
        static CBPingRequestPacket()
        {
            Register<CBPingRequestPacket>(new(ProtocolDirection.Clientbound, ProtocolStage.Configuration,0x05), Deserialize);
            Register<CBPingRequestPacket>(new(ProtocolDirection.Clientbound, ProtocolStage.Game,0x3E), Deserialize);
        }
        
        public required int Id { get; set; }
        
        public override void Serialize(Stream stream, PacketContext context)
        {
            base.Serialize(stream, context);
            stream.WriteS32(Id);
        }
        new private static Packet Deserialize(Stream stream, PacketContext context)
        {
            throw new NotImplementedException();
        }
    }
}