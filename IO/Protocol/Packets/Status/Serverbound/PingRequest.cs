using DivaMC.Common.IO.Serialization;

namespace DivaMC.Common.IO.Protocol.Packets.Status.Serverbound
{
    public sealed record PingRequest : Packet
    {
        static PingRequest()
        {
            PacketSignature signature = new PacketSignature(
                ProtocolDirection.Serverbound,
                ProtocolStage.Status,
                0x01
            );
            Packet.Register<PingRequest>(signature, Deserialize);
        }
        
        public required long Data { get; set; }
        
        protected override void Serialize(Stream stream, Context context)
        {
            throw new NotImplementedException();
        }
        private static Packet Deserialize(Stream stream, Context context)
        {
            return new PingRequest()
            {
                Data = stream.ReadS64(),
            };
        }
    }
}