using DivaMC.Protocol.Chat.Components;
using DivaMC.Protocol.IO.Serialization;

namespace DivaMC.Protocol.IO.Protocol.Packets.Login
{
    public sealed class CBDisconnectPacket : Packet
    {
        static CBDisconnectPacket()
        {
            Register<CBDisconnectPacket>(new(ProtocolDirection.Clientbound, ProtocolStage.Login,0x00), Deserialize);
        }
        
        public required ChatComponent Message { get; set; }
        
        public override void Serialize(Stream stream, PacketContext context)
        {
            base.Serialize(stream, context);
            stream.WriteJSON(Message);
        }
        new private static Packet Deserialize(Stream stream, PacketContext context)
        {
            throw new NotImplementedException();
        }
    }
}