using DivaMC.Common.IO.Serialization;

namespace DivaMC.Common.IO.Protocol.Packets.Configuration.Clientbound
{
    public sealed record Transfer : Packet
    {
        static Transfer()
        {
            PacketSignature signature = new PacketSignature(
                ProtocolDirection.Clientbound,
                ProtocolStage.Configuration,
                0x0C
            );
            Packet.Register<Transfer>(signature, Deserialize);
        }
        
        public required string Address { get; init; }
        public required ushort Port { get; init; }
        
        protected override void Serialize(Stream stream, object registry)
        {
            stream.WriteT0AVS32(Address);
            stream.WriteVS32(Port);
        }
        private static Packet Deserialize(Stream stream, object registry)
        {
            throw new NotImplementedException();
        }
    }
}