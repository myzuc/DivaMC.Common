using DivaMC.Common.IO.Serialization;

namespace DivaMC.Common.IO.Protocol.Packets.Login.Serverbound
{
    public sealed record EncryptionResponsePacket : Packet
    {
        static EncryptionResponsePacket()
        {
            PacketSignature signature = new PacketSignature(
                ProtocolDirection.Serverbound,
                ProtocolStage.Login,
                0x01
            );
            Packet.Register<EncryptionResponsePacket>(signature, Deserialize);
        }
        
        public required byte[] EncryptedSecret { get; set; }
        public required byte[] EncryptedSample { get; set; }
        
        protected override void Serialize(Stream stream)
        {
            throw new NotImplementedException();
        }
        private static Packet Deserialize(Stream stream)
        {
            return new EncryptionResponsePacket()
            {
                EncryptedSecret = stream.ReadU8A(stream.ReadVS32()),
                EncryptedSample = stream.ReadU8A(stream.ReadVS32())
            };
        }
    }
}