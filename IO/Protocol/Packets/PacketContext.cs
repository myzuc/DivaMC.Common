using DivaMC.Common.Common;

namespace DivaMC.Common.IO.Protocol.Packets
{
    public sealed record PacketContext
    {
        public required ProtocolDirection ProtocolDirection { get; set; }
        public required ProtocolStage ProtocolStage { get; set; }
        public required ConnectionContext ConnectionContext { get; set; }
        
        public PacketContext()
        {
            
        }

        public void Check(PacketSignature signature)
        {
            if (signature.Direction != ProtocolDirection || signature.Stage != ProtocolStage)
            {
                throw new InvalidOperationException();
            }
        }
    }
}