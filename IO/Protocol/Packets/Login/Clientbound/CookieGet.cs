using DivaMC.Common.Primitives;
using DivaMC.Common.IO.Serialization;

namespace DivaMC.Common.IO.Protocol.Packets.Login.Clientbound
{
    public sealed record CookieGet : Packet
    {
        static CookieGet()
        {
            PacketSignature signature = new PacketSignature(
                ProtocolDirection.Clientbound,
                ProtocolStage.Login,
                0x05
            );
            Packet.Register<CookieGet>(signature, Deserialize);
        }
        
        public required Identifier Id { get; init; }
        
        protected override void Serialize(Stream stream, object registry)
        {
            stream.Write(Id);
        }
        private static Packet Deserialize(Stream stream, object registry)
        {
            throw new NotImplementedException();
        }
    }
}