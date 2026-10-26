using DivaMC.Common.Common;
using DivaMC.Common.IO.Serialization;

namespace DivaMC.Common.IO.Protocol.Packets.Login.Clientbound
{
    public sealed record CookieGetPacket : Packet
    {
        static CookieGetPacket()
        {
            PacketSignature signature = new PacketSignature(
                ProtocolDirection.Clientbound,
                ProtocolStage.Login,
                0x05
            );
            Packet.Register<CookieGetPacket>(signature, Deserialize);
        }
        
        public required Identifier Id { get; set; }
        
        protected override void Serialize(Stream stream)
        {
            stream.Write(Id);
        }
        private static Packet Deserialize(Stream stream)
        {
            throw new NotImplementedException();
        }
    }
}