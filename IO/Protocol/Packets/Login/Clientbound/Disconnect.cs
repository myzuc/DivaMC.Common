using DivaMC.Common.Game.Chat.Components;

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
        
        public required ChatComponent Message { get; init; }
        
        protected override void Serialize(Stream stream, object registry)
        {
            stream.WriteJSON(Message);
        }
        private static Packet Deserialize(Stream stream, object registry)
        {
            throw new NotImplementedException();
        }
    }
}