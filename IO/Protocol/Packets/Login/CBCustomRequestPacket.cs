using DivaMC.Protocol.Common;
using DivaMC.Protocol.IO.Serialization;

namespace DivaMC.Protocol.IO.Protocol.Packets.Login
{
    public sealed class CBCustomRequestPacket : Packet
    {
        static CBCustomRequestPacket()
        {
            Register<CBCustomRequestPacket>(new(ProtocolDirection.Clientbound, ProtocolStage.Login,0x04), Deserialize);
        }
        
        public required int Id { get; set; }
        public required Identifier Channel { get; set; }
        public required byte[] Data { get; set; }
        
        public override void Serialize(Stream stream, PacketContext context)
        {
            base.Serialize(stream, context);
            stream.WriteVS32(Id);
            stream.Write(Channel);
            stream.WriteU8A(Data);
        }
        new private static Packet Deserialize(Stream stream, PacketContext context)
        {
            throw new NotImplementedException();
        }
    }
}