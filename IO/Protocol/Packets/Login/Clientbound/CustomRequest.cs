using DivaMC.Common.Common;
using DivaMC.Common.IO.Serialization;

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
        
        public required int Id { get; set; }
        public required Identifier Channel { get; set; }
        public required byte[] Data { get; set; }
        
        protected override void Serialize(Stream stream, Context context)
        {
            stream.WriteVS32(Id);
            stream.Write(Channel);
            stream.WriteU8A(Data);
        }
        private static Packet Deserialize(Stream stream, Context context)
        {
            throw new NotImplementedException();
        }
    }
}