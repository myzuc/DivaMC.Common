using DivaMC.Common.Primitives;

namespace DivaMC.Common.IO.Protocol.Packets.Login.Clientbound
{
    public sealed record CustomRequest : Packet
    {
        static CustomRequest()
        {
            PacketSignature signature = new PacketSignature(
                ProtocolDirection.Clientbound,
                ProtocolStage.Login,
                0x04
            );
            Packet.Register<CustomRequest>(signature, Deserialize);
        }
        
        public required int Id { get; init; }
        public required Identifier Channel { get; init; }
        public required byte[] Data { get; init; }
        
        protected override void Serialize(Stream stream, object registry)
        {
            stream.WriteVS32(Id);
            stream.Write(Channel);
            stream.WriteU8A(Data);
        }
        private static Packet Deserialize(Stream stream, object registry)
        {
            throw new NotImplementedException();
        }
    }
}