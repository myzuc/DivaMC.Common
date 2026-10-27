using DivaMC.Common.IO.Serialization;

namespace DivaMC.Common.IO.Protocol.Packets.Configuration.Clientbound
{
    public sealed class RegistryTagsPacket : Packet
    {
        static RegistryTagsPacket()
        {
            Register<RegistryTagsPacket>(Signature, Deserialize);
        }
        
        new public static readonly PacketSignature Signature = new PacketSignature(
            ProtocolDirection.Clientbound,
            ProtocolStage.Configuration,
            0x0E
        );
        
        public required List<Registries.RegistryTags> RegistryTagLists { get; init; }
        
        public override void Serialize(Stream stream, PacketContext context)
        {
            base.Serialize(stream, context);
            context.Check(Signature);
            stream.WriteVS32(RegistryTagLists.Count);
            foreach (Registries.RegistryTags registryTagList in RegistryTagLists)
            {
                stream.Write(registryTagList);
            }
        }
        new private static Packet Deserialize(Stream stream, PacketContext context)
        {
            context.Check(Signature);
            return new RegistryTagsPacket()
            {
                RegistryTagLists = Enumerable.Range(0, stream.ReadVS32()).Select(i => stream.Read<Registries.RegistryTags>()).ToList()
            };
        }
    }
}