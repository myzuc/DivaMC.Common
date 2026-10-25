using DivaMC.Common.IO.Serialization;

namespace DivaMC.Common.IO.Protocol.Packets.Login.Serverbound
{
    public sealed record EncryptionResponse : Packet
    {
        static EncryptionResponse()
        {
            PacketSignature signature = new PacketSignature(
                ProtocolDirection.Serverbound,
                ProtocolStage.Login,
                0x01
            );
            Packet.Register<EncryptionResponse>(signature, Deserialize);
        }
        
        public required byte[] EncryptedSecret { get; init; }
        public required byte[] EncryptedSample { get; init; }
        
        protected override void Serialize(Stream stream, object registry)
        {
            throw new NotImplementedException();
        }
        private static Packet Deserialize(Stream stream, object registry)
        {
            return new EncryptionResponse()
            {
                EncryptedSecret = stream.ReadU8A(stream.ReadVS32()),
                EncryptedSample = stream.ReadU8A(stream.ReadVS32())
            };
        }
    }
}