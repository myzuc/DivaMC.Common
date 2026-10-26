using DivaMC.Common.IO.Serialization;

namespace DivaMC.Common.IO.Protocol.Packets.Configuration.Clientbound
{
    public sealed record TransferPacket : Packet
    {
        static TransferPacket()
        {
            PacketSignature signature = new PacketSignature(
                ProtocolDirection.Clientbound,
                ProtocolStage.Configuration,
                0x0C
            );
            Packet.Register<TransferPacket>(signature, Deserialize);
        }
        
        public required string Address { get; set; }
        public required ushort Port { get; set; }
        
        protected override void Serialize(Stream stream)
        {
            stream.WriteT0AVS32(Address);
            stream.WriteVS32(Port);
        }
        private static Packet Deserialize(Stream stream)
        {
            throw new NotImplementedException();
        }
    }
}