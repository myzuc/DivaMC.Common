using DivaMC.Common.IO.Serialization;

namespace DivaMC.Common.IO.Protocol.Packets.Login
{
    public sealed class CBCompressionPacket : Packet
    {
        static CBCompressionPacket()
        {
            Register<CBCompressionPacket>(new(ProtocolDirection.Clientbound, ProtocolStage.Login,0x03), Deserialize);
        }
        
        public required int Threshold { get; set; }
        
        public override void Serialize(Stream stream, PacketContext context)
        {
            base.Serialize(stream, context);
            stream.WriteVS32(Threshold);
        }
        new private static Packet Deserialize(Stream stream, PacketContext context)
        {
            throw new NotImplementedException();
        }
    }
}