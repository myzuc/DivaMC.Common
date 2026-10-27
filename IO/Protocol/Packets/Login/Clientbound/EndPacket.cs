using DivaMC.Common.IO.Serialization;
using DivaMC.Common.Player.Common;

namespace DivaMC.Common.IO.Protocol.Packets.Login.Clientbound
{
    public sealed class EndPacket : Packet
    {
        static EndPacket()
        {
            Register<EndPacket>(Signature, Deserialize);
        }
        
        new public static readonly PacketSignature Signature = new PacketSignature(
            ProtocolDirection.Clientbound,
            ProtocolStage.Login,
            0x02
        );
        
        public required Profile Profile { get; set; }
        public required Guid SessionId { get; set; }
        
        public override void Serialize(Stream stream, PacketContext context)
        {
            base.Serialize(stream, context);
            context.Check(Signature);
            stream.Write(Profile);
            stream.WriteGuid(SessionId);
        }
        new private static Packet Deserialize(Stream stream, PacketContext context)
        {
            throw new NotImplementedException();
        }
    }
}