using DivaMC.Common.Common;
using DivaMC.Common.IO.Serialization;

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
        
        public required Identifier Id { get; set; }
        public required byte[]? Data { get; set; }
        
        protected override void Serialize(Stream stream)
        {
            throw new NotImplementedException();
        }
        private static Packet Deserialize(Stream stream)
        {
            return new CookieSet()
            {
                Id = stream.Read<Identifier>(),
                Data = stream.ReadU1() ? stream.ReadU8A(stream.ReadVS32()) : null
            };
        }
    }
}