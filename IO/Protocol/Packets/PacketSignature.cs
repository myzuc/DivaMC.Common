namespace Net.Myzuc.DivaMC.Common.IO.Protocol.Packets
{
    public readonly struct PacketSignature
    {
        public readonly ProtocolDirection Direction;
        public readonly ProtocolStage Stage;
        public readonly int Id;
        public PacketSignature(ProtocolDirection direction, ProtocolStage stage, int id)
        {
            Direction = direction;
            Stage = stage;
            Id = id;
        }
        public override int GetHashCode()
        {
            uint value = 0;
            value |= ((uint)Id & 0x0FFFFFFF) << 4;
            value |= ((uint)Stage & 0x07) << 1;
            value |= ((uint)Direction & 0x01) << 0;
            //0..0, 1..3, 4..31 
            return (int)value;
        }
    }
}