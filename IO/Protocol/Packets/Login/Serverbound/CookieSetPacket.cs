using DivaMC.Common.Common;
using DivaMC.Common.IO.Serialization;

namespace DivaMC.Common.IO.Protocol.Packets.Login.Serverbound
{
    public sealed class CookieSetPacket : Packet
    {
        static CookieSetPacket()
        {
            Register<CookieSetPacket>(Signature, Deserialize);
        }
        
        new public static readonly PacketSignature Signature = new PacketSignature(
            ProtocolDirection.Serverbound,
            ProtocolStage.Login,
            0x04
        );
        
        public required Identifier Id { get; set; }
        public required byte[]? Data { get; set; }
        
        public override void Serialize(Stream stream, PacketContext context)
        {
            throw new NotImplementedException();
        }
        new private static Packet Deserialize(Stream stream, PacketContext context)
        {
            context.Check(Signature);
            return new CookieSetPacket()
            {
                Id = stream.Read<Identifier>(),
                Data = stream.ReadU1() ? stream.ReadU8A(stream.ReadVS32()) : null
            };
        }
    }
}