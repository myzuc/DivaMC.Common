using DivaMC.Common.IO.Serialization;

namespace DivaMC.Common.IO.Protocol.Packets.Configuration.Clientbound
{
    public sealed record RulesAcceptRequest : Packet
    {
        static RulesAcceptRequest()
        {
            PacketSignature signature = new PacketSignature(
                ProtocolDirection.Clientbound,
                ProtocolStage.Configuration,
                0x14
            );
            Packet.Register<RulesAcceptRequest>(signature, Deserialize);
        }
        
        public required string Message { get; set; }
        
        protected override void Serialize(Stream stream, object registry)
        {
            stream.WriteT0AVS32(Message);
        }
        private static Packet Deserialize(Stream stream, object registry)
        {
            throw new NotImplementedException();
        }
    }
}