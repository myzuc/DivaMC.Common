using DivaMC.Common.IO.Serialization;
using DivaMC.Common.Registries;

namespace DivaMC.Common.IO.Protocol.Packets.Configuration.Clientbound
{
    public sealed class RegistryDataPacket : Packet
    {
        static RegistryDataPacket()
        {
            Register<RegistryDataPacket>(Signature, Deserialize);
        }
        
        new public static readonly PacketSignature Signature = new PacketSignature(
            ProtocolDirection.Clientbound,
            ProtocolStage.Configuration,
            0x07
        );
        
        public required Registry Registry { get; set; }
        
        public override void Serialize(Stream stream, PacketContext context)
        {
            base.Serialize(stream, context);
            context.Check(Signature);
            stream.Write(Registry);
        }
        new private static Packet Deserialize(Stream stream, PacketContext context)
        {
            context.Check(Signature);
            return new RegistryDataPacket()
            {
                Registry = stream.Read<Registry>()
            };
        }
    }
}