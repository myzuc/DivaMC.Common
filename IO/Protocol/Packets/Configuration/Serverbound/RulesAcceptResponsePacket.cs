using DivaMC.Common.IO.Serialization;

namespace DivaMC.Common.IO.Protocol.Packets.Configuration.Serverbound
{
    public sealed class RulesAcceptResponsePacket : Packet
    {
        static RulesAcceptResponsePacket()
        {
            Register<RulesAcceptResponsePacket>(Signature, Deserialize);
        }
        
        new public static readonly PacketSignature Signature = new PacketSignature(
            ProtocolDirection.Serverbound,
            ProtocolStage.Configuration,
            0x09
        );
        
        public override void Serialize(Stream stream, PacketContext context)
        {
            base.Serialize(stream, context);
            context.Check(Signature);
        }
        new private static Packet Deserialize(Stream stream, PacketContext context)
        {
            return new RulesAcceptResponsePacket();
        }
    }
}