using DivaMC.Protocol.IO.Serialization;
using DivaMC.Protocol.World.Events.GameEvents;

namespace DivaMC.Protocol.IO.Protocol.Packets.Game
{
    public sealed class CBGameEventPacket : Packet
    {
        static CBGameEventPacket()
        {
            Register<CBGameEventPacket>(new(ProtocolDirection.Clientbound, ProtocolStage.Game,0x27), Deserialize);
        }
        
        public required GameEvent GameEvent { get; set; }
        
        public override void Serialize(Stream stream, PacketContext context)
        {
            base.Serialize(stream, context);
            stream.Write(GameEvent);
        }
        new private static Packet Deserialize(Stream stream, PacketContext context)
        {
            throw new NotImplementedException();
        }
    }
}