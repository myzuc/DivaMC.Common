using DivaMC.Common.IO.Serialization;

namespace DivaMC.Common.IO.Protocol.Packets.Login.Serverbound
{
    public sealed class EncryptionResponsePacket : Packet
    {
        static EncryptionResponsePacket()
        {
            Register<EncryptionResponsePacket>(Signature, Deserialize);
        }
        
        new public static readonly PacketSignature Signature = new PacketSignature(
            ProtocolDirection.Serverbound,
            ProtocolStage.Login,
            0x01
        );
        
        public required byte[] EncryptedSecret { get; set; }
        public required byte[] EncryptedSample { get; set; }
        
        public override void Serialize(Stream stream, PacketContext context)
        {
            throw new NotImplementedException();
        }
        new private static Packet Deserialize(Stream stream, PacketContext context)
        {
            context.Check(Signature);
            return new EncryptionResponsePacket()
            {
                EncryptedSecret = stream.ReadU8A(stream.ReadVS32()),
                EncryptedSample = stream.ReadU8A(stream.ReadVS32())
            };
        }
    }
}