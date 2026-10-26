using DivaMC.Common.IO.Serialization;

namespace DivaMC.Common.IO.Protocol.Packets.Configuration.Clientbound
{
    public sealed record RegistryTagsPacket : Packet
    {
        static RegistryTagsPacket()
        {
            PacketSignature signature = new PacketSignature(
                ProtocolDirection.Clientbound,
                ProtocolStage.Configuration,
                0x0E
            );
            Packet.Register<RegistryTagsPacket>(signature, Deserialize);
        }
        
        public required List<Registries.RegistryTags> RegistryTagLists { get; init; }
        
        protected override void Serialize(Stream stream)
        {
            stream.WriteVS32(RegistryTagLists.Count);
            foreach (Registries.RegistryTags registryTagList in RegistryTagLists)
            {
                stream.Write(registryTagList);
            }
        }
        private static Packet Deserialize(Stream stream)
        {
            return new RegistryTagsPacket()
            {
                RegistryTagLists = Enumerable.Range(0, stream.ReadVS32()).Select(i => stream.Read<Registries.RegistryTags>()).ToList()
            };
        }
    }
}