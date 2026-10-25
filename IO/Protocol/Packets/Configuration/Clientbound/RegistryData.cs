using DivaMC.Common.IO.Serialization;
using DivaMC.Common.Registries;

namespace DivaMC.Common.IO.Protocol.Packets.Configuration.Clientbound
{
    public sealed record RegistryData : Packet
    {
        static RegistryData()
        {
            PacketSignature signature = new PacketSignature(
                ProtocolDirection.Clientbound,
                ProtocolStage.Configuration,
                0x07
            );
            Packet.Register<RegistryData>(signature, Deserialize);
        }
        
        public required Registry Registry { get; set; }
        
        protected override void Serialize(Stream stream, Context context)
        {
            stream.Write(Registry);
        }
        private static Packet Deserialize(Stream stream, Context context)
        {
            return new RegistryData()
            {
                Registry = stream.Read<Registry>()
            };
        }
    }
}