using DivaMC.Common.Common;
using DivaMC.Common.IO.Serialization;

namespace DivaMC.Common.IO.Protocol.Packets.Login.Clientbound
{
    public sealed class CookieGetPacket : Packet
    {
        static CookieGetPacket()
        {
            Register<CookieGetPacket>(Signature, Deserialize);
        }
        
        new public static readonly PacketSignature Signature = new PacketSignature(
            ProtocolDirection.Clientbound,
            ProtocolStage.Login,
            0x05
        );
        
        public required Identifier Id { get; set; }
        
        public override void Serialize(Stream stream, PacketContext context)
        {
            base.Serialize(stream, context);
            context.Check(Signature);
            stream.Write(Id);
        }
        new private static Packet Deserialize(Stream stream, PacketContext context)
        {
            throw new NotImplementedException();
        }
    }
}