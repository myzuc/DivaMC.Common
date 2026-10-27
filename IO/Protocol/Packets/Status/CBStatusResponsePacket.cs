using DivaMC.Protocol.IO.Serialization;

namespace DivaMC.Protocol.IO.Protocol.Packets.Status
{
    public sealed class CBStatusResponsePacket : Packet
    {
        static CBStatusResponsePacket()
        {
            Register<CBStatusResponsePacket>(new(ProtocolDirection.Clientbound, ProtocolStage.Status,0x00), Deserialize);
        }
        
        public required Chat.Status.Status Status { get; set; }
        
        public override void Serialize(Stream stream, PacketContext context)
        {
            base.Serialize(stream, context);
            stream.WriteJSON(Status);
        }
        new private static Packet Deserialize(Stream stream, PacketContext context)
        {
            throw new NotImplementedException();
        }
    }
}