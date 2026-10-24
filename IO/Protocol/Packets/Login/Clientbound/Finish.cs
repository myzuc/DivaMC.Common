using DivaMC.Common.Game.Objects;

namespace DivaMC.Common.IO.Protocol.Packets.Login.Clientbound
{
    public sealed record Finish : Packet
    {
        static Finish()
        {
            PacketSignature signature = new PacketSignature(
                ProtocolDirection.Clientbound,
                ProtocolStage.Login,
                0x02
            );
            Packet.Register<Finish>(signature, Deserialize);
        }
        
        public required Profile Profile { get; init; }
        public required Guid SessionId { get; init; }
        
        protected override void Serialize(Stream stream, object registry)
        {
            stream.Write(Profile);
            stream.WriteGuid(SessionId);
        }
        private static Packet Deserialize(Stream stream, object registry)
        {
            throw new NotImplementedException();
        }
    }
}