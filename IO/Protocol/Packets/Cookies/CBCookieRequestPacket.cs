using DivaMC.Protocol.Common;
using DivaMC.Protocol.IO.Serialization;

namespace DivaMC.Protocol.IO.Protocol.Packets.Cookies
{
    public sealed class CBCookieRequestPacket : Packet
    {
        static CBCookieRequestPacket()
        {
            Register<CBCookieRequestPacket>(new(ProtocolDirection.Clientbound, ProtocolStage.Login,0x05), Deserialize);
            Register<CBCookieRequestPacket>(new(ProtocolDirection.Clientbound, ProtocolStage.Configuration,0x00), Deserialize);
            Register<CBCookieRequestPacket>(new(ProtocolDirection.Clientbound, ProtocolStage.Game,0x15), Deserialize);
        }
        
        public required Identifier Id { get; set; }
        
        public override void Serialize(Stream stream, PacketContext context)
        {
            base.Serialize(stream, context);
            stream.Write(Id);
        }
        new private static Packet Deserialize(Stream stream, PacketContext context)
        {
            throw new NotImplementedException();
        }
    }
}