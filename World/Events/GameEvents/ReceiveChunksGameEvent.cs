namespace DivaMC.Protocol.World.Events.GameEvents
{
    public sealed record ReceiveChunksGameEvent : GameEvent
    {
        protected override byte Type => 13;

        public ReceiveChunksGameEvent()
        {
            
        }
    }
}