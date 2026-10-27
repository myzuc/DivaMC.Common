using System.Text.Json;
using DivaMC.Common.IO.Serialization;

namespace DivaMC.Common.IO.Protocol.Packets.Status.Clientbound
{
    public sealed class StatusResponsePacket : Packet
    {
        static StatusResponsePacket()
        {
            Register<StatusResponsePacket>(Signature, Deserialize);
        }
    
        new public static readonly PacketSignature Signature = new PacketSignature(
            ProtocolDirection.Clientbound,
            ProtocolStage.Status,
            0x00
        );
        
        public required Chat.Status.Status Status { get; set; }
        
        public override void Serialize(Stream stream, PacketContext context)
        {
            base.Serialize(stream, context);
            context.Check(Signature);
            stream.WriteJSON(Status);
        }
        new private static Packet Deserialize(Stream stream, PacketContext context)
        {
            throw new NotImplementedException();
        }
    }
}