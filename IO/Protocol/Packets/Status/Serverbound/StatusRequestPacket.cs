namespace DivaMC.Common.IO.Protocol.Packets.Status.Serverbound
{
    public sealed class StatusRequestPacket : Packet
    {
        static StatusRequestPacket()
        {
            Register<StatusRequestPacket>(Signature, Deserialize);
        }
    
        new public static readonly PacketSignature Signature = new PacketSignature(
            ProtocolDirection.Serverbound,
            ProtocolStage.Status,
            0x00
        );
        
        public override void Serialize(Stream stream, PacketContext context)
        {
            throw new NotImplementedException();
        }
        new private static Packet Deserialize(Stream stream, PacketContext context)
        {
            context.Check(Signature);
            return new StatusRequestPacket();
        }
    }
}