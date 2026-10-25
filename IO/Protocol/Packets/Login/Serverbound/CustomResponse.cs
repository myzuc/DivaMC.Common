using DivaMC.Common.IO.Serialization;

namespace DivaMC.Common.IO.Protocol.Packets.Login.Serverbound
{
    public sealed record CustomResponse : Packet
    {
        static CustomResponse()
        {
            PacketSignature signature = new PacketSignature(
                ProtocolDirection.Serverbound,
                ProtocolStage.Login,
                0x02
            );
            Packet.Register<CustomResponse>(signature, Deserialize);
        }
        
        public required int Id { get; init; }
        public required byte[]? Data { get; init; }
        
        protected override void Serialize(Stream stream, object registry)
        {
            throw new NotImplementedException();
        }
        private static Packet Deserialize(Stream stream, object registry)
        {
            return new CustomResponse()
            {
                Id = stream.ReadVS32(),
                Data = stream.ReadU1() ? stream.ReadU8A(stream.ReadVS32()) : null
            };
        }
    }
}