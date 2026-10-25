using DivaMC.Common.IO.Serialization;

namespace DivaMC.Common.IO.Protocol.Packets.Login.Clientbound
{
    public sealed record EncryptionRequest : Packet
    {
        static EncryptionRequest()
        {
            PacketSignature signature = new PacketSignature(
                ProtocolDirection.Clientbound,
                ProtocolStage.Login,
                0x01
            );
            Packet.Register<EncryptionRequest>(signature, Deserialize);
        }
        
        public required string ServerId { get; init; }
        public required byte[] PublicKey { get; init; }
        public required byte[] DecryptedSample { get; init; }
        public required bool Authenticate { get; init; }
        
        protected override void Serialize(Stream stream, object registry)
        {
            stream.WriteT0AVS32(ServerId);
            stream.WriteVS32(PublicKey.Length);
            stream.WriteU8A(PublicKey);
            stream.WriteVS32(DecryptedSample.Length);
            stream.WriteU8A(DecryptedSample);
            stream.WriteU1(Authenticate);
        }
        private static Packet Deserialize(Stream stream, object registry)
        {
            throw new NotImplementedException();
        }
    }
}