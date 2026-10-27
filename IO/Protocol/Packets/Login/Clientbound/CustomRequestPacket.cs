using DivaMC.Common.Common;
using DivaMC.Common.IO.Serialization;

namespace DivaMC.Common.IO.Protocol.Packets.Login.Clientbound
{
    public sealed class CustomRequestPacket : Packet
    {
        static CustomRequestPacket()
        {
            Register<CustomRequestPacket>(Signature, Deserialize);
        }
        
        new public static readonly PacketSignature Signature = new PacketSignature(
            ProtocolDirection.Clientbound,
            ProtocolStage.Login,
            0x04
        );
        
        public required int Id { get; set; }
        public required Identifier Channel { get; set; }
        public required byte[] Data { get; set; }
        
        public override void Serialize(Stream stream, PacketContext context)
        {
            base.Serialize(stream, context);
            context.Check(Signature);
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