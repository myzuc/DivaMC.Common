using DivaMC.Protocol.IO.Serialization;

namespace DivaMC.Protocol.IO.Protocol.Packets.Login
{
    public sealed class CBEncryptionRequestPacket : Packet
    {
        static CBEncryptionRequestPacket()
        {
            Register<CBEncryptionRequestPacket>(new(ProtocolDirection.Clientbound, ProtocolStage.Login,0x01), Deserialize);
        }
        
        public required string ServerId { get; set; }
        public required byte[] PublicKey { get; set; }
        public required byte[] DecryptedSample { get; set; }
        public required bool Authenticate { get; set; }
        
        public override void Serialize(Stream stream, PacketContext context)
        {
            base.Serialize(stream, context);
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