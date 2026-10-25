using DivaMC.Common.IO.Serialization;
using DivaMC.Common.Primitives;

namespace DivaMC.Common.IO.Protocol.Packets.Login.Serverbound
{
    public sealed record CookieSet : Packet
    {
        static CookieSet()
        {
            PacketSignature signature = new PacketSignature(
                ProtocolDirection.Serverbound,
                ProtocolStage.Login,
                0x04
            );
            Packet.Register<CookieSet>(signature, Deserialize);
        }
        
        public required Identifier Id { get; init; }
        public required byte[]? Data { get; init; }
        
        protected override void Serialize(Stream stream, object registry)
        {
            throw new NotImplementedException();
        }
        private static Packet Deserialize(Stream stream, object registry)
        {
            return new CookieSet()
            {
                Id = stream.Read<Identifier>(),
                Data = stream.ReadU1() ? stream.ReadU8A(stream.ReadVS32()) : null
            };
        }
    }
}