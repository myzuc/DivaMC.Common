namespace DivaMC.Protocol.IO.Protocol.Packets
{
    public readonly record struct PacketSignature
    {
        public readonly ProtocolDirection Direction { get; }
        public readonly ProtocolStage Stage { get; }
        public readonly int Id { get; }
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