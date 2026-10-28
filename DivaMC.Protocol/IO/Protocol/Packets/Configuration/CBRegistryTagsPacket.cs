using DivaMC.Protocol.IO.Serialization;

namespace DivaMC.Protocol.IO.Protocol.Packets.Configuration
{
    public sealed class CBRegistryTagsPacket : Packet
    {
        static CBRegistryTagsPacket()
        {
            Register<CBRegistryTagsPacket>(new(ProtocolDirection.Clientbound, ProtocolStage.Configuration,0x0E), Deserialize);
        }
        
        public required List<Registries.RegistryTags> RegistryTagLists { get; init; }
        
        public override void Serialize(Stream stream, PacketContext context)
        {
            base.Serialize(stream, context);
            stream.WriteVS32(RegistryTagLists.Count);
            foreach (Registries.RegistryTags registryTagList in RegistryTagLists)
            {
                stream.Write(registryTagList);
            }
        }
        new private static Packet Deserialize(Stream stream, PacketContext context)
        {
            return new CBRegistryTagsPacket()
            {
                RegistryTagLists = Enumerable.Range(0, stream.ReadVS32()).Select(i => stream.Read<Registries.RegistryTags>()).ToList()
            };
        }
    }
}