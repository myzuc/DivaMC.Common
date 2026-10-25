using System.Text.Json;
using DivaMC.Common.IO.Serialization;

namespace DivaMC.Common.IO.Protocol.Packets.Status.Clientbound
{
    public sealed record StatusResponse : Packet
    {
        static StatusResponse()
        {
            PacketSignature signature = new PacketSignature(
                ProtocolDirection.Clientbound,
                ProtocolStage.Status,
                0x00
            );
            Packet.Register<StatusResponse>(signature, Deserialize);
        }
        
        public required Game.Chat.Status.Status Status { get; set; }
        
        protected override void Serialize(Stream stream, object registry)
        {
            stream.WriteJSON(Status);
        }
        private static Packet Deserialize(Stream stream, object registry)
        {
            throw new NotImplementedException();
        }
    }
}