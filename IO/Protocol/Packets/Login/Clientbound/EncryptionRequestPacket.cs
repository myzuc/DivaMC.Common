using DivaMC.Common.IO.Serialization;

namespace DivaMC.Common.IO.Protocol.Packets.Login.Clientbound
{
    public sealed class EncryptionRequestPacket : Packet
    {
        static EncryptionRequestPacket()
        {
            Register<EncryptionRequestPacket>(Signature, Deserialize);
        }
        
        new public static readonly PacketSignature Signature = new PacketSignature(
            ProtocolDirection.Clientbound,
            ProtocolStage.Login,
            0x01
        );
        
        public required string ServerId { get; set; }
        public required byte[] PublicKey { get; set; }
        public required byte[] DecryptedSample { get; set; }
        public required bool Authenticate { get; set; }
        
        public override void Serialize(Stream stream, PacketContext context)
        {
            base.Serialize(stream, context);
            context.Check(Signature);
            stream.WriteT0AVS32(ServerId);
            stream.WriteVS32(PublicKey.Length);
            stream.WriteU8A(PublicKey);
            stream.WriteVS32(DecryptedSample.Length);
            stream.WriteU8A(DecryptedSample);
            stream.WriteU1(Authenticate);
        }
        new private static Packet Deserialize(Stream stream, PacketContext context)
        {
            throw new NotImplementedException();
        }
    }
}