using DivaMC.Protocol.Common;
using DivaMC.Protocol.IO.Serialization;

namespace DivaMC.Protocol.IO.Protocol.Packets.Cookies
{
    public sealed class SBCookieResponsePacket : Packet
    {
        static SBCookieResponsePacket()
        {
            Register<SBCookieResponsePacket>(new(ProtocolDirection.Serverbound, ProtocolStage.Login,0x04), Deserialize);
            Register<SBCookieResponsePacket>(new(ProtocolDirection.Serverbound, ProtocolStage.Configuration,0x01), Deserialize);
            Register<SBCookieResponsePacket>(new(ProtocolDirection.Serverbound, ProtocolStage.Game,0x15), Deserialize);
        }
        
        public required Identifier Id { get; set; }
        public required byte[]? Data { get; set; }
        
        public override void Serialize(Stream stream, PacketContext context)
        {
            throw new NotImplementedException();
        }
        new private static Packet Deserialize(Stream stream, PacketContext context)
        {
            return new SBCookieResponsePacket()
            {
                Id = stream.Read<Identifier>(),
                Data = stream.ReadU1() ? stream.ReadU8A(stream.ReadVS32()) : null
            };
        }
    }
}