using DivaMC.Common.IO.Serialization;

namespace DivaMC.Common.IO.Protocol.Packets.Configuration.Clientbound
{
    public sealed class TransferPacket : Packet
    {
        static TransferPacket()
        {
            Register<TransferPacket>(Signature, Deserialize);
        }
        
        new public static readonly PacketSignature Signature = new PacketSignature(
            ProtocolDirection.Clientbound,
            ProtocolStage.Configuration,
            0x0C
        );
        
        public required string Address { get; set; }
        public required ushort Port { get; set; }
        
        public override void Serialize(Stream stream, PacketContext context)
        {
            base.Serialize(stream, context);
            context.Check(Signature);
            stream.WriteT0AVS32(Address);
            stream.WriteVS32(Port);
        }
        new private static Packet Deserialize(Stream stream, PacketContext context)
        {
            throw new NotImplementedException();
        }
    }
}