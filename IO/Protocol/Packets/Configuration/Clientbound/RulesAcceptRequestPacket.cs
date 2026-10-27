using DivaMC.Common.IO.Serialization;

namespace DivaMC.Common.IO.Protocol.Packets.Configuration.Clientbound
{
    public sealed class RulesAcceptRequestPacket : Packet
    {
        static RulesAcceptRequestPacket()
        {
            Register<RulesAcceptRequestPacket>(Signature, Deserialize);
        }
        
        new public static readonly PacketSignature Signature = new PacketSignature(
            ProtocolDirection.Clientbound,
            ProtocolStage.Configuration,
            0x14
        );
        
        public required string Message { get; set; }
        
        public override void Serialize(Stream stream, PacketContext context)
        {
            base.Serialize(stream, context);
            context.Check(Signature);
            stream.WriteT0AVS32(Message);
        }
        new private static Packet Deserialize(Stream stream, PacketContext context)
        {
            throw new NotImplementedException();
        }
    }
}