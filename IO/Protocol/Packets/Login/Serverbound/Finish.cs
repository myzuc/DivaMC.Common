namespace DivaMC.Common.IO.Protocol.Packets.Login.Serverbound
{
    public sealed record Finish : Packet
    {
        static Finish()
        {
            PacketSignature signature = new PacketSignature(
                ProtocolDirection.Serverbound,
                ProtocolStage.Login,
                0x03
            );
            Packet.Register<Finish>(signature, Deserialize);
        }
        
        protected override void Serialize(Stream stream, Context context)
        {
            
        }
        private static Packet Deserialize(Stream stream, Context context)
        {
            return new Finish();
        }
    }
}