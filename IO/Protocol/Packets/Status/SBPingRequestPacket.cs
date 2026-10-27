using DivaMC.Common.IO.Serialization;

namespace DivaMC.Common.IO.Protocol.Packets.Status
{
    public sealed class SBPingRequestPacket : Packet
    {
        static SBPingRequestPacket()
        {
            Register<SBPingRequestPacket>(new(ProtocolDirection.Serverbound, ProtocolStage.Status,0x01), Deserialize);
        }
        
        public required long Data { get; set; }
        
        public override void Serialize(Stream stream, PacketContext context)
        {
            throw new NotImplementedException();
        }
        new private static Packet Deserialize(Stream stream, PacketContext context)
        {
            return new SBPingRequestPacket()
            {
                Data = stream.ReadS64(),
            };
        }
    }
}