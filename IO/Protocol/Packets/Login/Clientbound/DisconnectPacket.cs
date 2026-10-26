using DivaMC.Common.Chat.Components;
using DivaMC.Common.IO.Serialization;

namespace DivaMC.Common.IO.Protocol.Packets.Login.Clientbound
{
    public sealed record DisconnectPacket : Packet
    {
        static DisconnectPacket()
        {
            PacketSignature signature = new PacketSignature(
                ProtocolDirection.Clientbound,
                ProtocolStage.Login,
                0x00
            );
            Packet.Register<DisconnectPacket>(signature, Deserialize);
        }
        
        public required ChatComponent Message { get; set; }
        
        protected override void Serialize(Stream stream)
        {
            stream.WriteJSON(Message);
        }
        private static Packet Deserialize(Stream stream)
        {
            throw new NotImplementedException();
        }
    }
}