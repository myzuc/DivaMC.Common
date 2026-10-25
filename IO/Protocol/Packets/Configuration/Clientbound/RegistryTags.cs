using DivaMC.Common.Game.Registries;
using DivaMC.Common.IO.Serialization;

namespace DivaMC.Common.IO.Protocol.Packets.Configuration.Clientbound
{
    public sealed record RegistryTags : Packet
    {
        static RegistryTags()
        {
            PacketSignature signature = new PacketSignature(
                ProtocolDirection.Clientbound,
                ProtocolStage.Configuration,
                0x0E
            );
            Packet.Register<RegistryTags>(signature, Deserialize);
        }
        
        public required IReadOnlyCollection<Game.Registries.RegistryTags> RegistryTagLists { get; init; }
        
        protected override void Serialize(Stream stream, object registry)
        {
            stream.WriteVS32(RegistryTagLists.Count);
            foreach (Game.Registries.RegistryTags registryTagList in RegistryTagLists)
            {
                stream.Write(registryTagList);
            }
        }
        private static Packet Deserialize(Stream stream, object registry)
        {
            return new RegistryTags()
            {
                RegistryTagLists = Enumerable.Range(0, stream.ReadVS32()).Select(i => stream.Read<Game.Registries.RegistryTags>()).ToArray()
            };
        }
    }
}