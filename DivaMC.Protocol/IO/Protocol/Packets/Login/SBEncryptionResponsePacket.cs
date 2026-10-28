using DivaMC.Protocol.IO.Serialization;

namespace DivaMC.Protocol.IO.Protocol.Packets.Login
{
    public sealed class SBEncryptionResponsePacket : Packet
    {
        static SBEncryptionResponsePacket()
        {
            Register<SBEncryptionResponsePacket>(new(ProtocolDirection.Serverbound, ProtocolStage.Login,0x01), Deserialize);
        }
        
        public required byte[] EncryptedSecret { get; set; }
        public required byte[] EncryptedSample { get; set; }
        
        public override void Serialize(Stream stream, PacketContext context)
        {
            throw new NotImplementedException();
        }
        new private static Packet Deserialize(Stream stream, PacketContext context)
        {
            return new SBEncryptionResponsePacket()
            {
                EncryptedSecret = stream.ReadU8A(stream.ReadVS32()),
                EncryptedSample = stream.ReadU8A(stream.ReadVS32())
            };
        }
    }
}