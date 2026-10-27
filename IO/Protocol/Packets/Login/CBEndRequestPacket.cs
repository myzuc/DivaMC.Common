using DivaMC.Common.IO.Serialization;
using DivaMC.Common.Player.Common;

namespace DivaMC.Common.IO.Protocol.Packets.Login
{
    public sealed class CBEndRequestPacket : Packet
    {
        static CBEndRequestPacket()
        {
            Register<CBEndRequestPacket>(new(ProtocolDirection.Clientbound, ProtocolStage.Login,0x02), Deserialize);
        }
        
        public required Profile Profile { get; set; }
        public required Guid SessionId { get; set; }
        
        public override void Serialize(Stream stream, PacketContext context)
        {
            base.Serialize(stream, context);
            stream.Write(Profile);
            stream.WriteGuid(SessionId);
        }
        new private static Packet Deserialize(Stream stream, PacketContext context)
        {
            throw new NotImplementedException();
        }
    }
}