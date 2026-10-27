using DivaMC.Common.IO.Serialization;

namespace DivaMC.Common.IO.Protocol.Packets.Status.Serverbound
{
    public sealed class PingRequestPacket : Packet
    {
        static PingRequestPacket()
        {
            Register<PingRequestPacket>(Signature, Deserialize);
        }
    
        new public static readonly PacketSignature Signature = new PacketSignature(
            ProtocolDirection.Serverbound,
            ProtocolStage.Status,
            0x01
        );
        
        public required long Data { get; set; }
        
        public override void Serialize(Stream stream, PacketContext context)
        {
            throw new NotImplementedException();
        }
        new private static Packet Deserialize(Stream stream, PacketContext context)
        {
            context.Check(Signature);
            return new PingRequestPacket()
            {
                Data = stream.ReadS64(),
            };
        }
    }
}