namespace DivaMC.Common.IO.Protocol
{
    public enum ProtocolStage : byte
    {
        Handshake = 0x00,
        Status = 0x01,
        Login = 0x02,
        Configuration = 0x03,
        Game = 0x04,
    }
}