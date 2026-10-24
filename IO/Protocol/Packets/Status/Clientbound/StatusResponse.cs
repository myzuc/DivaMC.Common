using System.Text.Json;

namespace Net.Myzuc.DivaMC.Common.IO.Protocol.Packets.Status.Clientbound
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
        
        public required Game.Chat.Status.Status Status { get; init; }
        
        protected override void Serialize(Stream stream, object registry)
        {
            stream.WriteT0AVS32(JsonSerializer.Serialize(Status, Global.JsonSerializerOptions));
        }
        private static Packet Deserialize(Stream stream, object registry)
        {
            throw new NotImplementedException();
        }
    }
}