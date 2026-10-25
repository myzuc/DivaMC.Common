using DivaMC.Common.IO.Serialization;
using DivaMC.Common.Player.Common;

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
        
        public required Profile Profile { get; set; }
        public required Guid SessionId { get; set; }
        
        protected override void Serialize(Stream stream, Context context)
        {
            stream.Write(Profile);
            stream.WriteGuid(SessionId);
        }
        private static Packet Deserialize(Stream stream, Context context)
        {
            throw new NotImplementedException();
        }
    }
}