using DivaMC.Common.IO.Serialization;

namespace DivaMC.Common.IO.Protocol.Packets.Login
{
    public sealed class SBCustomResponsePacket : Packet
    {
        static SBCustomResponsePacket()
        {
            Register<SBCustomResponsePacket>(new(ProtocolDirection.Serverbound, ProtocolStage.Login,0x02), Deserialize);
        }
        
        public required int Id { get; set; }
        public required byte[]? Data { get; set; }
        
        public override void Serialize(Stream stream, PacketContext context)
        {
            throw new NotImplementedException();
        }
        new private static Packet Deserialize(Stream stream, PacketContext context)
        {
            return new SBCustomResponsePacket()
            {
                Id = stream.ReadVS32(),
                Data = stream.ReadU1() ? stream.ReadU8A(stream.ReadVS32()) : null
            };
        }
    }
}