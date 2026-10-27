namespace DivaMC.Common.World.Events.GameEvents
{
    public sealed record RespawnFailureGameEvent : GameEvent
    {
        protected override byte Type => 0;
        
        public RespawnFailureGameEvent()
        {
            
        }
    }
}