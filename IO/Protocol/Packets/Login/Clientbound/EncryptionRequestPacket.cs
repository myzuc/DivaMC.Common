using DivaMC.Common.IO.Serialization;

namespace DivaMC.Common.IO.Protocol.Packets.Login.Clientbound
{
    public sealed record EncryptionRequestPacket : Packet
    {
        static EncryptionRequestPacket()
        {
            PacketSignature signature = new PacketSignature(
                ProtocolDirection.Clientbound,
                ProtocolStage.Login,
                0x01
            );
            Packet.Register<EncryptionRequestPacket>(signature, Deserialize);
        }
        
        public required string ServerId { get; set; }
        public required byte[] PublicKey { get; set; }
        public required byte[] DecryptedSample { get; set; }
        public required bool Authenticate { get; set; }
        
        protected override void Serialize(Stream stream)
        {
            stream.WriteT0AVS32(ServerId);
            stream.WriteVS32(PublicKey.Length);
            stream.WriteU8A(PublicKey);
            stream.WriteVS32(DecryptedSample.Length);
            stream.WriteU8A(DecryptedSample);
            stream.WriteU1(Authenticate);
        }
        private static Packet Deserialize(Stream stream)
        {
            throw new NotImplementedException();
        }
    }
}