using DivaMC.Common.IO.Serialization;

namespace DivaMC.Common.IO.Protocol.Packets.Common
{
    public sealed class CBTransferPacket : Packet
    {
        static CBTransferPacket()
        {
            Register<CBTransferPacket>(new(ProtocolDirection.Clientbound, ProtocolStage.Configuration,0x0C), Deserialize);
            Register<CBTransferPacket>(new(ProtocolDirection.Clientbound, ProtocolStage.Game,0x84), Deserialize);
        }
        
        public required string Address { get; set; }
        public required ushort Port { get; set; }
        
        public override void Serialize(Stream stream, PacketContext context)
        {
            base.Serialize(stream, context);
            stream.WriteT0AVS32(Address);
            stream.WriteVS32(Port);
        }
        new private static Packet Deserialize(Stream stream, PacketContext context)
        {
            throw new NotImplementedException();
        }
    }
}