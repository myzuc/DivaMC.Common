using DivaMC.Common.IO.Serialization;

namespace DivaMC.Common.IO.Protocol.Packets.Configuration.Serverbound
{
    public sealed record Finish : Packet
    {
        static Finish()
        {
            PacketSignature signature = new PacketSignature(
                ProtocolDirection.Serverbound,
                ProtocolStage.Configuration,
                0x03
            );
            Packet.Register<Finish>(signature, Deserialize);
        }
        
        protected override void Serialize(Stream stream)
        {
        }
        private static Packet Deserialize(Stream stream)
        {
            return new Finish();
        }
    }
}