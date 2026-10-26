using System.Text.Json;
using DivaMC.Common.IO.Serialization;

namespace DivaMC.Common.IO.Protocol.Packets.Status.Clientbound
{
    public sealed record StatusResponsePacket : Packet
    {
        static StatusResponsePacket()
        {
            PacketSignature signature = new PacketSignature(
                ProtocolDirection.Clientbound,
                ProtocolStage.Status,
                0x00
            );
            Packet.Register<StatusResponsePacket>(signature, Deserialize);
        }
        
        public required Chat.Status.Status Status { get; set; }
        
        protected override void Serialize(Stream stream)
        {
            stream.WriteJSON(Status);
        }
        private static Packet Deserialize(Stream stream)
        {
            throw new NotImplementedException();
        }
    }
}