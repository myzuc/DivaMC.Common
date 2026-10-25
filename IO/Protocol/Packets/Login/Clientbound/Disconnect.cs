using DivaMC.Common.Chat.Components;
using DivaMC.Common.IO.Serialization;

namespace DivaMC.Common.IO.Protocol.Packets.Login.Clientbound
{
    public sealed record Disconnect : Packet
    {
        static Disconnect()
        {
            PacketSignature signature = new PacketSignature(
                ProtocolDirection.Clientbound,
                ProtocolStage.Login,
                0x00
            );
            Packet.Register<Disconnect>(signature, Deserialize);
        }
        
        public required ChatComponent Message { get; set; }
        
        protected override void Serialize(Stream stream, Context context)
        {
            stream.WriteJSON(Message);
        }
        private static Packet Deserialize(Stream stream, Context context)
        {
            throw new NotImplementedException();
        }
    }
}