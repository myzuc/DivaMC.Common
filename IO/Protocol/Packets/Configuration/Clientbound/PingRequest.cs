using DivaMC.Common.IO.Serialization;

namespace DivaMC.Common.IO.Protocol.Packets.Configuration.Clientbound
{
    public sealed record PingRequest : Packet
    {
        static PingRequest()
        {
            PacketSignature signature = new PacketSignature(
                ProtocolDirection.Clientbound,
                ProtocolStage.Configuration,
                0x05
            );
            Packet.Register<PingRequest>(signature, Deserialize);
        }
        
        public required int Id { get; set; }
        
        protected override void Serialize(Stream stream, Context context)
        {
            stream.WriteS32(Id);
        }
        private static Packet Deserialize(Stream stream, Context context)
        {
            throw new NotImplementedException();
        }
    }
}