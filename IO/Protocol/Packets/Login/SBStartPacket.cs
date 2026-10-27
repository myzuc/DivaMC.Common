using DivaMC.Protocol.IO.Serialization;

namespace DivaMC.Protocol.IO.Protocol.Packets.Login
{
    public sealed class SBStartPacket : Packet
    {
        static SBStartPacket()
        {
            Register<SBStartPacket>(new(ProtocolDirection.Serverbound, ProtocolStage.Login,0x00), Deserialize);
        }
        
        public required string Name { get; set; }
        public required Guid Id { get; set; }
        
        public override void Serialize(Stream stream, PacketContext context)
        {
            throw new NotImplementedException();
        }
        new private static Packet Deserialize(Stream stream, PacketContext context)
        {
            return new SBStartPacket()
            {
                Name = stream.ReadT0AVS32(),
                Id = stream.ReadGuid(),
            };
        }
    }
}