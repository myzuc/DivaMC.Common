namespace DivaMC.Common.World.Events.GameEvents
{
    public sealed record RainStartGameEvent : GameEvent
    {
        protected override byte Type => 2;
        
        public RainStartGameEvent()
        {
            
        }
    }
}