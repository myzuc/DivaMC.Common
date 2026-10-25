using DivaMC.Common.Game.Registries;
using DivaMC.Common.IO.Serialization;

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
        
        public required Registry Registry { get; init; }
        
        protected override void Serialize(Stream stream, object registry)
        {
            stream.Write(Registry);
        }
        private static Packet Deserialize(Stream stream, object registry)
        {
            return new RegistryData()
            {
                Registry = stream.Read<Registry>()
            };
        }
    }
}