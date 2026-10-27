namespace DivaMC.Common.World.Events.GameEvents
{
    public sealed record RainEndGameEvent : GameEvent
    {
        protected override byte Type => 1;
        
        public RainEndGameEvent()
        {
            
        }
    }
}