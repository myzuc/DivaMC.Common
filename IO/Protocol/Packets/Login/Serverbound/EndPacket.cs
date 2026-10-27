namespace DivaMC.Common.IO.Protocol.Packets.Login.Serverbound
{
    public sealed class EndPacket : Packet
    {
        static EndPacket()
        {
            Register<EndPacket>(Signature, Deserialize);
        }
        
        new public static readonly PacketSignature Signature = new PacketSignature(
            ProtocolDirection.Serverbound,
            ProtocolStage.Login,
            0x03
        );
        
        public override void Serialize(Stream stream, PacketContext context)
        {
            base.Serialize(stream, context);
            context.Check(Signature);
        }
        new private static Packet Deserialize(Stream stream, PacketContext context)
        {
            context.Check(Signature);
            return new EndPacket();
        }
    }
}