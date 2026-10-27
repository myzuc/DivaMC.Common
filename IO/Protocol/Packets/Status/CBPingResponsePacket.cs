using DivaMC.Common.IO.Serialization;

namespace DivaMC.Common.IO.Protocol.Packets.Status
{
    public sealed class CBPingResponsePacket : Packet
    {
        static CBPingResponsePacket()
        {
            Register<CBPingResponsePacket>(new(ProtocolDirection.Clientbound, ProtocolStage.Status,0x01), Deserialize);
        }
        
        public required long Data { get; set; }
        
        public override void Serialize(Stream stream, PacketContext context)
        {
            base.Serialize(stream, context);
            stream.WriteS64(Data);
        }
        new private static Packet Deserialize(Stream stream, PacketContext context)
        {
            throw new NotImplementedException();
        }
    }
}