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
        
        public required byte[] EncryptedSecret { get; set; }
        public required byte[] EncryptedSample { get; set; }
        
        protected override void Serialize(Stream stream, Context context)
        {
            throw new NotImplementedException();
        }
        private static Packet Deserialize(Stream stream, Context context)
        {
            return new EncryptionResponse()
            {
                EncryptedSecret = stream.ReadU8A(stream.ReadVS32()),
                EncryptedSample = stream.ReadU8A(stream.ReadVS32())
            };
        }
    }
}