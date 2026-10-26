using DivaMC.Common.IO.Serialization;
using DivaMC.Common.Registries;

namespace DivaMC.Common.IO.Protocol.Packets.Configuration.Clientbound
{
    public sealed record RegistryDataPacket : Packet
    {
        static RegistryDataPacket()
        {
            PacketSignature signature = new PacketSignature(
                ProtocolDirection.Clientbound,
                ProtocolStage.Configuration,
                0x07
            );
            Packet.Register<RegistryDataPacket>(signature, Deserialize);
        }
        
        public required Registry Registry { get; set; }
        
        protected override void Serialize(Stream stream)
        {
            stream.Write(Registry);
        }
        private static Packet Deserialize(Stream stream)
        {
            return new RegistryDataPacket()
            {
                Registry = stream.Read<Registry>()
            };
        }
    }
}