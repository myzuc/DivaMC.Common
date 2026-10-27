using DivaMC.Common.IO.Serialization;
using DivaMC.Common.World.Events.GameEvents;

namespace DivaMC.Common.IO.Protocol.Packets.Play.Clientbound
{
    public sealed class GameEventPacket : Packet
    {
        static GameEventPacket()
        {
            Register<GameEventPacket>(Signature, Deserialize);
        }
    
        new public static readonly PacketSignature Signature = new PacketSignature(
            ProtocolDirection.Clientbound,
            ProtocolStage.Play,
            0x27
        );
        
        public required GameEvent GameEvent { get; set; }
        
        public override void Serialize(Stream stream, PacketContext context)
        {
            base.Serialize(stream, context);
            context.Check(Signature);
            stream.Write(GameEvent);
        }
        new private static Packet Deserialize(Stream stream, PacketContext context)
        {
            throw new NotImplementedException();
        }
    }
}