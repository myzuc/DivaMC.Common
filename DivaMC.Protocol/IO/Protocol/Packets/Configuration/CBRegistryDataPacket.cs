using DivaMC.Protocol.IO.Serialization;
using DivaMC.Protocol.Registries;

namespace DivaMC.Protocol.IO.Protocol.Packets.Configuration
{
    public sealed class CBRegistryDataPacket : Packet
    {
        static CBRegistryDataPacket()
        {
            Register<CBRegistryDataPacket>(new(ProtocolDirection.Clientbound, ProtocolStage.Configuration,0x07), Deserialize);
        }
        
        public required Registry Registry { get; set; }
        
        public override void Serialize(Stream stream, PacketContext context)
        {
            base.Serialize(stream, context);
            stream.Write(Registry);
        }
        new private static Packet Deserialize(Stream stream, PacketContext context)
        {
            return new CBRegistryDataPacket()
            {
                Registry = stream.Read<Registry>()
            };
        }
    }
}