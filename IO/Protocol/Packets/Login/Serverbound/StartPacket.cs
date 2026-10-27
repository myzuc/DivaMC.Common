using DivaMC.Common.IO.Serialization;

namespace DivaMC.Common.IO.Protocol.Packets.Login.Serverbound
{
    public sealed class StartPacket : Packet
    {
        static StartPacket()
        {
            Register<StartPacket>(Signature, Deserialize);
        }
        
        new public static readonly PacketSignature Signature = new PacketSignature(
            ProtocolDirection.Serverbound,
            ProtocolStage.Login,
            0x00
        );
        
        public required string Name { get; set; }
        public required Guid Id { get; set; }
        
        public override void Serialize(Stream stream, PacketContext context)
        {
            throw new NotImplementedException();
        }
        new private static Packet Deserialize(Stream stream, PacketContext context)
        {
            context.Check(Signature);
            return new StartPacket()
            {
                Name = stream.ReadT0AVS32(),
                Id = stream.ReadGuid(),
            };
        }
    }
}